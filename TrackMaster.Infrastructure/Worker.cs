using TrackMaster.Core;
using TrackMaster.Core.Services.SessionManagement;

namespace TrackMaster.Infrastructure;

public class Worker : BackgroundService
{
    private readonly ISessionMonitorService _sessionMonitorService;
    private readonly ILogger<Worker> _logger;

    public Worker(ISessionMonitorService sessionMonitorService,ILogger<Worker> logger)
    {
        _sessionMonitorService = sessionMonitorService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("TrackMaster.Infrastructure Worker iniciado");
        await _sessionMonitorService.StartAsync(stoppingToken);
        _logger.LogInformation("TrackMaster.Infrastructure Worker detenido");       
    }
}
