using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMaster.Core.Models
{
    [FirestoreData]
    public class LapTime
    {
        [FirestoreProperty] public List<int> Sectors { get; set; }
        [FirestoreProperty]public int Total {  get; set; }

        public LapTime(List<int> sectors)
        {
            Sectors = sectors.ToList();
            foreach (var sector in sectors)
            {
                Total += sector;
            }
        }

        public LapTime() { }

        public override string ToString()
        {
            string str = "";

            for(int i = 0; i < Sectors.Count; i++)
            {
                str += $"Sector {i + 1}: {Sectors[i]}, ";
            }

            str += $"Total: {Total}";

            return str;
        }
    }
}
