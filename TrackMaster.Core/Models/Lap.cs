using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMaster.Core.Models
{
    [FirestoreData]
    public class Lap
    {
        [FirestoreProperty] public int LapNumber {  get; set; }
        [FirestoreProperty]public bool Box {  get; set; }
        [FirestoreProperty]public LapTime LapTime { get; set; }
        [FirestoreProperty]public List<CarInput> CarInputs { get; set; } = new() { new CarInput() };
        [FirestoreProperty]public double[] TyreWear { get; set; } = new double[4];
        [FirestoreProperty]public double Fuel {  get; set; }

        public Lap(int lapNumber, bool box, LapTime lapTime, List<CarInput> carInputs, double[] tyres, float fuel) 
        {
            LapNumber = lapNumber;
            Box = box;
            LapTime = lapTime;
            CarInputs = carInputs;
            TyreWear = tyres;
            Fuel = fuel;
        }

        public Lap() { }
    }
}
