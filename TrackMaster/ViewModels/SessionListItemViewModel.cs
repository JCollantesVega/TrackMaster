using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMaster.Core.Models;

namespace TrackMaster.ViewModels
{
    public class SessionListItemViewModel
    {
        private readonly Session _session;

        public SessionListItemViewModel(Session session)
        {
            _session = session;
        }

        public string Track => _session.Track;
        public DateTime DateTime => _session.DateTime;
        public string SessionType => _session.SessionType;
    }
}
