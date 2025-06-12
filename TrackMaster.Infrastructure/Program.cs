using TrackMaster.Core.Services.SessionManagement;
using TrackMaster.Infrastructure.Services.Persistence;
using TrackMaster.Infrastructure;
using TrackMaster.Core.Services.Persistence;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton(provider => FirestoreDbFactory.Create());


builder.Services.AddSingleton<ISessionCacheService, SessionCacheService>();
builder.Services.AddSingleton<ISessionUploader, FirebaseSessionUploader>();
builder.Services.AddSingleton<ISessionRepository, FirebaseSessionRepository>();

// Inyectar el nuevo servicio de monitorización
builder.Services.AddSingleton<ISessionMonitorService, SessionMonitorService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
