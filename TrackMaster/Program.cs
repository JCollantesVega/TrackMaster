using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using TrackMaster.ViewModels;
using TrackMaster.Infrastructure.Services.Persistence;
using TrackMaster.Core.Services.Persistence;



namespace TrackMaster
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            BuildAvaloniaApp(serviceProvider).StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp(IServiceProvider serviceProvider)
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace()
                .UseReactiveUI()
            .AfterSetup(app =>
            {
                if(app.Instance is App realApp)
                {
                    realApp.InitializeWithServices(serviceProvider);
                }
            });

        private static void ConfigureServices(IServiceCollection services)
        {
            //var services = new ServiceCollection();

            //Registro de FirestoreDb
            services.AddSingleton(provider => FirestoreDbFactory.Create());

            //Registro de los servicios de persistencia
            services.AddSingleton<ISessionRepository, FirebaseSessionRepository>();
            services.AddSingleton<ISessionUploader, FirebaseSessionUploader>();

            // ViewModels
            services.AddSingleton<SessionsViewModel>();
            services.AddSingleton<TelemetryViewModel>();
            services.AddSingleton<StrategyViewModel>();
            services.AddSingleton<MainWindowViewModel>();

            // Factory
            services.AddSingleton<IViewModelFactory, ViewModelFactory>();


        }
    }
}
