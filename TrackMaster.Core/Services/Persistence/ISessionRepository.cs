using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMaster.Core.Models;

namespace TrackMaster.Core.Services.Persistence
{
    public interface ISessionRepository
    {
        Task<List<Session>> GetAllSessionsAsync();
        Task<Session?> GetSessionByIdAsync(string sessionId);
    }
}
