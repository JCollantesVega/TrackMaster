using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMaster.Core.Models
{
    [FirestoreData]
    public class Stint
    {
        [FirestoreProperty]public int StintNumber {  get; set; }
        [FirestoreProperty]public string TyreCompound {  get; set; }
        [FirestoreProperty]public List<Lap> Laps { get; set; }
        [FirestoreProperty] public double FuelLoad { get; set; }

    }
}
