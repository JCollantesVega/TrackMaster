using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMaster.Core.Models
{
    [FirestoreData]
    public class Session
    {
        [FirestoreProperty]public string SessionId {  get; set; }
        [FirestoreProperty] public string Track { get; set; }
        [FirestoreProperty] public string SessionType { get; set; }
        [FirestoreProperty] public DateTime DateTime { get; set; }

        [FirestoreProperty] public List<Stint> Stints { get; set; }

    }
}
