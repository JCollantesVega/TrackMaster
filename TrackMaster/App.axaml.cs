using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Google.Api;
using Microsoft.Extensions.DependencyInjection;
using System;
using TrackMaster.ViewModels;
using TrackMaster.Views;

namespace TrackMaster
{
    public partial class App : Application
    {
        private TrayIcon? _trayIcon;
        public IServiceProvider? _serviceProvider {  get; set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public App(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void InitializeWithServices(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public App() { }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
                var mainWindow = new MainWindow(mainWindowViewModel);
                desktop.MainWindow = mainWindow;

                _trayIcon = new TrayIcon
                {
                    Icon = new WindowIcon("../../../Assets/hello.ico"),
                    ToolTipText = "YO TENGO UNA MENTE CON 4 COMPUTADORA"
                };

                _trayIcon.Menu = new NativeMenu();

                var abrirMenuItem = new NativeMenuItem("Abrir");

                abrirMenuItem.Click += (_, __) =>
                {
                    if(!mainWindow.IsVisible)
                    {
                        mainWindow.Show();
                    }
                    mainWindow.Activate();
                };

                var salirMenuItem = new NativeMenuItem("Salir");

                salirMenuItem.Click += (_, __) =>
                {
                    _trayIcon?.Dispose();
                    desktop.Shutdown();
                };

                _trayIcon.Menu.Items.Add(abrirMenuItem);
                _trayIcon.Menu.Items.Add(salirMenuItem);

                _trayIcon.IsVisible = true;

                mainWindow.Hide();
            }

            base.OnFrameworkInitializationCompleted();
        }

    }
}