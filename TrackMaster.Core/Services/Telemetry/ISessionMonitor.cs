using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMaster.Core.Services.Telemetry
{
    internal interface ISessionMonitor
    {
        Task StartSessionMonitoring(CancellationToken token, int msRate);
    }
}
