using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestructureLibrary
{
    public static class ServiceProviders
    {
        private static ServiceCollection BuildServiceCollection(bool withCodeConfig)
        {
            var serviceCollection = new ServiceCollection();
            Serilog.Core.Logger seriLogger = null;
            if (withCodeConfig)
            {
                seriLogger = SeriLogUtil.CreateSeriloggerWithCodeConfig();
            }
            else
            {
                seriLogger = SeriLogUtil.CreateSeriloggerWithOutCodeConfig();
            }

            serviceCollection.AddLogging(loggingBuilder => {
                loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(seriLogger, dispose: true);
            });
            return serviceCollection;
        }

        public static AutofacServiceProvider CreateServiceProvider(bool withCodeConfig)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(BuildServiceCollection(withCodeConfig));

            var container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }
    }
}
