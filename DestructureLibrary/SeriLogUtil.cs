using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;
using System;
using System.IO;

namespace DestructureLibrary
{
    public class SeriLogUtil
    {
        public static Logger CreateSeriloggerWithCodeConfig()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("logSettings.json", false, true)
            .Build();

            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Destructure.With(new ResultDestructuringPolicy())
                .WriteTo
                    .File(new CompactJsonFormatter(),
                    Path.Combine(AppContext.BaseDirectory, "logs", "log-Json.txt"))
                .CreateLogger();
        }

        public static Logger CreateSeriloggerWithOutCodeConfig()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("logSettings.json", false, true)
            .Build();

            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Destructure.With(new ResultDestructuringPolicy())
                .CreateLogger();
        }
    }
}
