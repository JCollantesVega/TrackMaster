using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMaster.Core.Services.SessionManagement
{
    public interface ISessionMonitorService
    {
        Task StartAsync(CancellationToken token);
    }
}
