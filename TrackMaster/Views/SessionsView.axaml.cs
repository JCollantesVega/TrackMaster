using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TrackMaster.ViewModels;

namespace TrackMaster.Views;

public partial class SessionsView : UserControl
{
    public SessionsView()
    {
        InitializeComponent();
        DataContext = new SessionsViewModel();
    }
}