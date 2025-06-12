using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using TrackMaster.Core.Services.Persistence;

namespace TrackMaster.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {

        private readonly IViewModelFactory _viewModelFactory;
        

        [ObservableProperty]
        private ViewModelBase _currentPage;

        [ObservableProperty]
        private ListItemTemplate? _selectedListItem;

        partial void OnSelectedListItemChanged(ListItemTemplate? value)
        {
            if (value is null) return;
            CurrentPage = _viewModelFactory.CreateViewModel(value.ModelType);
        }
        public ObservableCollection<ListItemTemplate> Items { get; } = new()
        {
            new ListItemTemplate(typeof(SessionsViewModel)),
            new ListItemTemplate(typeof(TelemetryViewModel)),
            new ListItemTemplate(typeof(StrategyViewModel))
        };

        public MainWindowViewModel(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
            CurrentPage = _viewModelFactory.CreateViewModel(typeof(SessionsViewModel));
        }
    }

    public class ListItemTemplate 
    {
        public string Label { get; }
        public Type ModelType { get; }
        public ListItemTemplate(Type type)
        {
            ModelType = type;
            Label = type.Name.Replace("ViewModel", "");
        }

    }
}
