using AssettoCorsaSharedMemory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMaster.Core.Models;

namespace TrackMaster.Core.Services
{
    public class SharedMemoryReader 
    {
        private readonly AssettoCorsa _ac;
        private Session? _currentSession;
        private Stint? _currentStint;
        private int _stintCounter = 0;
        private Lap? _currentLap;
        private int _lastSector = -1;
        private bool _wasInPitLast = true;
        private List<CarInput> _currentInputs = new();
        private List<int> _sectors = new();
        private int _lastLapNumber = -1;
        private int _currentLapTimestamp = 0;

        public SharedMemoryReader()
        {
            _ac = new AssettoCorsa();
        }


        public bool isAssettoCorsaStarted()
        {
            return Process.GetProcessesByName("acs").Length > 0;
        }

        public bool connectToSharedMemory()
        {

            try
            {
                _ac.Start(); 
            }
            catch (Exception)
            {
                return false;
            }

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    var graphics = _ac.ReadGraphics();

                    if ((int)graphics.Status > 0)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                }

                Thread.Sleep(1000);
            }
            return false;
        }

        public async Task StartSessionMonitoring(CancellationToken token, int msRate)
        {
            while (!token.IsCancellationRequested)
            {
                if (!isAssettoCorsaStarted())
                {
                    break;
                }

                var graphics = _ac.ReadGraphics();
                var physics = _ac.ReadPhysics();
                var staticInfo = _ac.ReadStaticInfo();


                if(_currentSession == null)
                {
                    _currentSession = new Session
                    {
                        SessionId = $"{graphics.Session}_{DateTime.UtcNow:yyyyMMdd_HHmmss}",
                        Track = staticInfo.Track,
                        SessionType = graphics.Session.ToString(),
                        DateTime = DateTime.Now,
                        Stints = new()
                    };
                }

                //gestiona cuándo se realiza un stint
                if(graphics.IsInPit == 0 && _wasInPitLast)
                {
                    _currentStint = new Stint
                    {
                        StintNumber = _stintCounter++,
                        FuelLoad = Math.Round(physics.Fuel, 2),
                        TyreCompound = graphics.TyreCompound,
                        Laps = new()
                    };
                    _currentSession.Stints.Add(_currentStint);

                    _lastLapNumber = graphics.CompletedLaps;
                    _currentLap = new Lap { LapNumber = _lastLapNumber };
                    _lastSector = -1;
                    _currentInputs = new List<CarInput>();

                }

                //toma el sector actual
                int currentSector = graphics.CurrentSectorIndex;

                //controlar el cambio de sector
                if(_currentStint != null &&  _lastSector != currentSector)
                {

                    if(graphics.LastSectorTime != 0)
                    {
                        _sectors.Add(graphics.LastSectorTime);
                    }

                    if (currentSector == 0 && _lastSector == staticInfo.SectorCount-1) //fin de vuelta
                    {
                        _currentLap.LapNumber = graphics.CompletedLaps;
                        _currentLap.Fuel = Math.Round(physics.Fuel, 2);
                        _currentLap.LapTime = new LapTime(_sectors);
                        _currentLap.TyreWear = Array.ConvertAll(physics.TyreWear, f => (double)f);

                        _currentStint.Laps.Add(_currentLap);
                        _currentLap.CarInputs = _currentInputs;
                        _currentLapTimestamp = 0;

                        _currentInputs.Clear();
                        _sectors.Clear();
                        _currentLap = new Lap();
                    }

                    _lastSector = currentSector;
                }

                if(_currentStint != null)
                {
                    CarInput actualInput = new CarInput(_currentLapTimestamp+=msRate, (int)(physics.Gas * 100), (int)(physics.Brake * 100), (int)(physics.SteerAngle * 100), physics.Gear, [graphics.CarCoordinates[0], graphics.CarCoordinates[1]], Array.ConvertAll(physics.TyreCoreTemperature, t => (int)t));

                    _currentInputs.Add(actualInput);
                }

                _wasInPitLast = Convert.ToBoolean(graphics.IsInPit);

                await Task.Delay(msRate, token); // frecuencia de captura
            }
        }

        public Session? GetCompletedSession() => _currentSession;

        
    }

}
