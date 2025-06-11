using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMaster.Core.Models
{
    [FirestoreData]
    public class CarInput
    {
        [FirestoreProperty] public int TimeStamp {  get; set; }
        [FirestoreProperty]public int Gas { get; set; }
        [FirestoreProperty]public int Brake { get; set; }
        [FirestoreProperty]public int Steer {  get; set; }
        [FirestoreProperty]public int Gear { get; set; }
        [FirestoreProperty]public float[] CarCoordinates { get; set; }
        [FirestoreProperty]public int[] TyreTemperature { get; set; }

        public CarInput(int timeStamp, int gas, int brake, int steer, int gear, float[] carCoordinates, int[] tyreTemperatures)
        {
            TimeStamp = timeStamp;
            Gas = gas;
            Brake = brake;
            Steer = steer;
            Gear = gear;
            CarCoordinates = carCoordinates;
            TyreTemperature = tyreTemperatures;
        }

        public CarInput() { }

        public override string ToString()
        {
            return $"{TimeStamp}[Gas: {Gas}. Brake: {Brake}. Steer: {Steer}. Gear: {Gear}. Coordinates: ({CarCoordinates[0]}, {CarCoordinates[1]})]";
            
        }

    }
}
