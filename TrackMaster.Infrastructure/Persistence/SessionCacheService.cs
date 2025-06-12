using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMaster.Core.Models;

namespace TrackMaster.Core.Services.Persistence
{
    public class SessionCacheService : ISessionCacheService
    {
        public Session? CurrentSession { get; set; }
    }
}
