using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OSK.Serialization.Polymorphism.Internal.Services;
using OSK.Serialization.Polymorphism.Models;
using OSK.Serialization.Polymorphism.Ports;

namespace OSK.Serialization.Polymorphism
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPolymorphicSerialization(this IServiceCollection services)
        {
            services.AddTransient<IPolymorphismContextProvider, PolymorphismContextProvider>();

            return services;
        }

        public static IServiceCollection AddPolymorphismStrategy<TAttribute, TStrategy>(this IServiceCollection services)
            where TAttribute : PolymorphismAttribute
            where TStrategy : class, IPolymorphismStrategy
        {
            services.AddTransient(_ => new PolymorphismStrategyDescriptor()
            {
                PolymorphicAttributeType = typeof(TAttribute),
                PolymorphismStrategyType = typeof(TStrategy)
            });
            services.TryAddTransient<TStrategy>();

            return services;
        }
    }
}
