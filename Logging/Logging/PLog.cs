using Microsoft.Extensions.Logging;
using PartonetMLM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartonetMLM.Core.Logging
{
    public class PLog<T> where T : class
    {
        private readonly ILogger logger;
        public PLog()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
            });

            logger = loggerFactory.CreateLogger<T>();
        }

        public void Error(ProjectEnum project, string message)
        {
            logger.LogError("Example Error log message");
        }

        public void Warrning(ProjectEnum project, string message)
        {
            logger.LogWarning("Example Warrning log message");
        }

        public void Info(ProjectEnum project, string message)
        {
            logger.LogInformation("Example Information log message");
        }

    }
}
