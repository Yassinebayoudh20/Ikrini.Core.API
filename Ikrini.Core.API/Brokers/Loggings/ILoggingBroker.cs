// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Ikrini.Core.API.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        ValueTask LogInformationAsync(string message);
        ValueTask LogTraceAsync(string message);
        ValueTask LogDebugAsync(string message);
        ValueTask LogWarningAsync(string message);
        ValueTask LogErrorAsync(Exception exception);
        ValueTask LogCriticalAsync(Exception exception);
    }
}
