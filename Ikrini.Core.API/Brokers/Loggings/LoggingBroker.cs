// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------


using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Brokers.Loggings
{
    internal class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger)
        {
            this.logger = logger;
        }
        public async ValueTask LogInformationAsync(string message)
        {
            this.logger.LogInformation(message);
        }

        public async ValueTask LogTraceAsync(string message)
        {
            this.logger.LogTrace(message);
        }

        public async ValueTask LogDebugAsync(string message)
        {
            this.logger.LogDebug(message);
        }

        public async ValueTask LogWarningAsync(string message)
        {
            throw new NotImplementedException();
        }

        public async ValueTask LogErrorAsync(string message, Exception exception)
        {
            this.logger.LogError(exception, message);
        }

        public async ValueTask LogCriticalAsync(Exception exception)
        {
            this.logger.LogCritical(exception,exception.Message);
        }
    }
}
