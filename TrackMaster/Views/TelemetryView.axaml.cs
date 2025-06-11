using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TrackMaster.ViewModels;

namespace TrackMaster.Views;

public partial class TelemetryView : UserControl
{
    public TelemetryView()
    {
        InitializeComponent();
        DataContext = new TelemetryViewModel();
    }
}