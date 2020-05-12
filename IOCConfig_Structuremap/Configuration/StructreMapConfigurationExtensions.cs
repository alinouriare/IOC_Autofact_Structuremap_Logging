using IOCConfig_Structuremap.NewFolder;
using IOCConfig_Structuremap.Service;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOCConfig_Structuremap.Configuration
{
    public static class StructreMapConfigurationExtensions
    {


        #region AddContainerFromAssembly--ContainerAddService
        public static IServiceProvider ConfigureStructureMap(this IServiceCollection services)
        {
            var Dll = typeof(Calculator).Assembly;
            var container = new Container();
            container.Configure(c =>
            {
                c.Scan(scanner =>
                {
                    scanner.Assembly(Dll);
                    scanner.WithDefaultConventions();


                });

                c.Populate(services);
            });


            return container.GetInstance<IServiceProvider>();


        }
        #endregion

    }
}
