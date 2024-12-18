using System;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rsp.Logging.Interceptors;

namespace Rsp.Logging.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers an interceptor for interceptable services in the DI container.
    /// Services implementing the <see cref="IInterceptable"/> interface will have proxies generated
    /// to enable method interception using the specified interceptor.
    /// </summary>
    /// <typeparam name="TInterceptor">The type of the interceptor that implements <see cref="IAsyncInterceptor"/>.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with interceptors applied.</returns>
    public static IServiceCollection AddLoggingInterceptor<TInterceptor>(this IServiceCollection services)
        where TInterceptor : class, IAsyncInterceptor
    {
        // Ensures that the required services for proxy generation and interception are available.
        // 1. Register IProxyGenerator as a singleton, ensuring only one instance is shared across the app.
        // 2. Register the interceptor itself as a transient service so a new instance is created when needed.
        services.TryAddSingleton<IProxyGenerator, ProxyGenerator>();
        services.TryAddSingleton<TInterceptor>();

        // Iterate through all the currently registered service descriptors in the service collection.
        // Using a standard 'for' loop to allow modification of the services list in place (no need for extra memory).
        for (int serviceIndex = 0; serviceIndex < services.Count; serviceIndex++)
        {
            var serviceDescriptor = services[serviceIndex]; // Retrieve the current service descriptor.

            // Check if the service descriptor is for an interceptable service.
            // An interceptable service:
            //   - Has a ServiceType that is an interface.
            //   - Has an ImplementationType that is not null (concrete implementation exists).
            //   - Implements the IInterceptable marker interface.
            if (IsInterceptable(serviceDescriptor))
            {
                // Replace the current service descriptor with a new one that wraps the implementation in a proxy.
                // This effectively adds interception behavior without changing the existing registrations elsewhere.
                services[serviceIndex] = new ServiceDescriptor
                (
                    serviceDescriptor.ServiceType, // Retain the original ServiceType.
                    provider => AddProxy<TInterceptor>(provider, serviceDescriptor), // Factory to create a proxy.
                    serviceDescriptor.Lifetime // Preserve the original service lifetime (Singleton, Scoped, or Transient).
                );
            }
        }

        // Return the updated service collection to allow method chaining.
        return services;
    }

    /// <summary>
    /// Determines whether a given service descriptor is interceptable.
    /// An interceptable service must implement the <see cref="IInterceptable"/> interface,
    /// have a concrete implementation type, and its service type must be an interface.
    /// </summary>
    /// <param name="descriptor">The <see cref="ServiceDescriptor"/> to evaluate.</param>
    /// <returns><c>true</c> if the service descriptor is interceptable; otherwise, <c>false</c>.</returns>
    private static bool IsInterceptable(ServiceDescriptor descriptor)
    {
        // Determines if a given service descriptor is eligible for interception.
        // This method checks the following conditions:
        // 1. The ServiceType must be an interface (proxies can only be created for interfaces).
        // 2. The ImplementationType must not be null (a concrete class implementation must exist).
        // 3. The ServiceType must implement the IInterceptable interface (used as a marker for interceptable services).
        return descriptor.ServiceType.IsInterface &&
               descriptor.ImplementationType is not null &&
               typeof(IInterceptable).IsAssignableFrom(descriptor.ServiceType);
    }

    /// <summary>
    /// Creates a proxy for an interceptable service.
    /// The proxy wraps the original service implementation and applies the specified interceptor
    /// to intercept method calls.
    /// </summary>
    /// <typeparam name="TInterceptor">The type of the interceptor that implements <see cref="IAsyncInterceptor"/>.</typeparam>
    /// <param name="provider">The <see cref="IServiceProvider"/> to resolve dependencies.</param>
    /// <param name="descriptor">The <see cref="ServiceDescriptor"/> containing the service registration details.</param>
    /// <returns>A proxy instance that wraps the original service implementation.</returns>
    private static object AddProxy<TInterceptor>(IServiceProvider provider, ServiceDescriptor descriptor)
        where TInterceptor : class, IAsyncInterceptor
    {
        // Resolves the required services to generate a proxy:
        // 1. IProxyGenerator: The Castle.Core proxy generator used to create proxy instances.
        // 2. TInterceptor: The interceptor that contains the interception logic.
        var proxyGenerator = provider.GetRequiredService<IProxyGenerator>();
        var interceptor = provider.GetRequiredService<TInterceptor>();

        // Create an instance of the service's original implementation using the DI container.
        // ActivatorUtilities.CreateInstance ensures that any constructor dependencies are resolved properly.
        var implementation = ActivatorUtilities.CreateInstance(provider, descriptor.ImplementationType!);

        // Use Castle.Core's proxy generator to create a dynamic proxy that wraps the original implementation.
        // The proxy intercepts calls to the interface's methods and routes them through the provided interceptor.
        return proxyGenerator.CreateInterfaceProxyWithTarget
        (
            descriptor.ServiceType, // The interface to be proxied.
            implementation,         // The original service implementation instance.
            interceptor             // The interceptor that adds interception behavior.
        );
    }
}