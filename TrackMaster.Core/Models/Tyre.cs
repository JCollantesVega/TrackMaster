using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMaster.Core.Models
{
    [FirestoreData]
    public class Tyre
    {
        [FirestoreProperty]public float Wear {  get; set; }
        [FirestoreProperty]public float Temperature { get; set; } //ignore in json
        public Tyre() 
        {
            Wear = 100f;
            Temperature = 0f;
        }
    }
}
