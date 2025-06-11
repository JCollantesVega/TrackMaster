using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TrackMaster.ViewModels;

namespace TrackMaster.Views;

public partial class StrategyView : UserControl
{
    public StrategyView()
    {
        InitializeComponent();
        DataContext = new StrategyViewModel();
    }
}