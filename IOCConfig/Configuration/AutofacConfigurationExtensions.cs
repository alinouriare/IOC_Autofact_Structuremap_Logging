using Autofac;
using Autofac.Extensions.DependencyInjection;
using IOCConfig.Contracts;
using IOCConfig.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOCConfig.Configuration
{
   public static class AutofacConfigurationExtensions
    {
        #region AddContainerFromAssembly
        public static void AddServices(this ContainerBuilder containerBuilder)
        {

            var Dll = typeof(Calculator).Assembly;

            containerBuilder.RegisterAssemblyTypes(Dll)
                .AssignableTo<IScopedDependency>
                ().AsImplementedInterfaces().
                InstancePerLifetimeScope();


            containerBuilder.RegisterAssemblyTypes(Dll)
                .AssignableTo<ITransientDependency>
                ().AsImplementedInterfaces().
                InstancePerDependency();


            containerBuilder.RegisterAssemblyTypes(Dll)
              .AssignableTo<ISingletonDependency>
              ().AsImplementedInterfaces().
              SingleInstance();

        }
        #endregion

        #region ContainerAddService
        public static IServiceProvider BuildAutofacServiceProvider(this IServiceCollection services)
        {

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder.AddServices();
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
        #endregion
    }
}
