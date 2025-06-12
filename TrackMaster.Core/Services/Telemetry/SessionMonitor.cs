using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMaster.Core.Models;

namespace TrackMaster.Core.Services.Telemetry
{
    public class SessionMonitor : ISessionMonitor
    {

        private readonly AssettoCorsaMemoryReader _reader;

        private Session? _currentSession;
        private Stint? _currentStint;
        private Lap? _currentLap;

        private int _stintCounter = 0;
        private int _lastSector = -1;
        private int _lastLapNumber = -1;
        private int _currentLapTimestamp = 0;
        private bool _wasInPitLast = true;
        private bool _visitedBox;

        private List<CarInput> _currentInputs = new List<CarInput>();
        private List<int> _sectors = new List<int>();

        public SessionMonitor(AssettoCorsaMemoryReader memoryReader)
        {
           _reader = memoryReader;
        }

        /// <summary>
        /// Comienza a leer datos y construir una sesión en tiempo real.
        /// </summary>
        public async Task StartSessionMonitoring(CancellationToken token, int msRate)
        {
            if (!_reader.Connect())
                throw new InvalidOperationException("No se pudo conectar con Assetto Corsa");

            while (!token.IsCancellationRequested)
            {
                if (!_reader.IsAssettoCorsaRunning())
                    break;

                var graphics = _reader.ReadGraphics();
                var physics = _reader.ReadPhysics();
                var staticInfo = _reader.ReadStaticInfo();

                
                //se encarga de iniciar una nueva sesión
                if(_currentSession == null)
                {
                    _currentSession = new Session
                    {
                        SessionId = $"{graphics.Session}_{DateTime.UtcNow:yyyyMMdd_HHmmss}",
                        Track = staticInfo.Track,
                        SessionType = graphics.Session.ToString(),
                        DateTime = DateTime.UtcNow,
                        Stints = new()
                    };
                }

                //Inicia un nuevo stint al salir de boxes
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

                int currentSector = graphics.CurrentSectorIndex;
                if(graphics.IsInPit == 1)
                {
                    _visitedBox = true;
                }

                //Detectar el cambio de sector
                if(_currentStint != null && _lastSector != currentSector)
                {
                    if(graphics.LastSectorTime != 0)
                        _sectors.Add(graphics.LastSectorTime);

                    if (currentSector == 0 && _lastSector == staticInfo.SectorCount - 1)
                    {
                        _currentLap.LapNumber = graphics.CompletedLaps;
                        _currentLap.Fuel = Math.Round(physics.Fuel, 2);
                        _currentLap.LapTime = new LapTime(_sectors);
                        _currentLap.Box = _visitedBox;
                        _currentLap.TyreWear = Array.ConvertAll(physics.TyreWear, f => (double)f);
                        _currentLap.CarInputs = _currentInputs;

                        _currentStint.Laps.Add(_currentLap);

                        _currentLapTimestamp = 0;
                        _currentInputs.Clear();
                        _sectors.Clear();
                        _currentLap = new Lap();
                        _visitedBox = false;
                    }

                    _lastSector = currentSector;
                }

                if(_currentStint != null)
                {
                    CarInput actualInput = new CarInput
                    {
                        TimeStamp = _currentLapTimestamp += msRate,
                        Gas = (int)(physics.Gas * 100),
                        Brake = (int)(physics.Brake * 100),
                        Steer = (int)(physics.SteerAngle * 100),
                        Gear = physics.Gear,
                        CarCoordinates = [graphics.CarCoordinates[0], graphics.CarCoordinates[1]],
                        TyreTemperature = Array.ConvertAll(physics.TyreCoreTemperature, f => (int)f)
                    };
                    _currentInputs.Add(actualInput);
                }
                _wasInPitLast = Convert.ToBoolean(graphics.IsInPit);

                await Task.Delay(msRate, token);

            }

            
        }

        /// <summary>
        /// Devuelve la sesión construida (si existe).
        /// </summary>
        public Session? GetCompletedSession() => _currentSession;
    }
}
