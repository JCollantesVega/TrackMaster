using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMaster.Core.Services.Telemetry;
using TrackMaster.Core.Services.Persistence;

namespace TrackMaster.Core.Services.SessionManagement
{
    public class SessionMonitorService : ISessionMonitorService
    {
        private readonly AssettoCorsaMemoryReader _reader;
        private readonly ISessionUploader _firebaseSessionUploader;

        public SessionMonitorService(ISessionUploader sessionUploader)
        {
            _reader = new AssettoCorsaMemoryReader();
            _firebaseSessionUploader = sessionUploader;
        }

        public async Task StartAsync(CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
                if (_reader.IsAssettoCorsaRunning())
                {
                    Console.WriteLine("Juego detectado");
                    ISessionMonitor sessionMonitor = new SessionMonitor(_reader);

                    await sessionMonitor.StartSessionMonitoring(token, 50);

                    var completedSession = ((SessionMonitor)sessionMonitor).GetCompletedSession();

                    if (completedSession != null)
                    {
                        Console.WriteLine("SESION TERMINADA");
                        Console.WriteLine($"Circuito: {completedSession.Track}, Fecha: {completedSession.DateTime}");
                        await _firebaseSessionUploader.UploadSessionAsync(completedSession);
                    }
                }
                else
                {
                    Console.WriteLine("Esperando al inicio de Assetto Corsa");
                }

                await Task.Delay(1000);
            }
        }
    }
}
