using AssettoCorsaSharedMemory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMaster.Core.Services.Telemetry
{
    public class AssettoCorsaMemoryReader
    {
        private readonly AssettoCorsa _ac;

        public AssettoCorsaMemoryReader()
        {
            _ac = new AssettoCorsa();
        }

        /// <summary>
        /// Intenta conectar con la memoria compartida de Assetto Corsa
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            try
            {
                _ac.Start();
            }
            catch
            {
                return false;
            }

            for (int i = 0; i < 50; i++)
            {
                try
                {
                    var graphics = _ac.ReadGraphics();
                    if ((int)graphics.Status > 0)
                    {
                        return true;
                    }
                }
                catch { }

                Thread.Sleep(1000);
            }
            return false;
        }


        /// <summary>
        /// Verifica si el proceso de Assetto Corsa está en ejecución
        /// </summary>
        public bool IsAssettoCorsaRunning()
        {
            return Process.GetProcessesByName("acs").Length>0;
        }

        public Graphics ReadGraphics()
        {
            return _ac.ReadGraphics();
        }

        public Physics ReadPhysics()
        {
            return _ac.ReadPhysics();
        }

        public StaticInfo ReadStaticInfo()
        {
            return _ac.ReadStaticInfo();
        }
    }
}
