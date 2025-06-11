using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace TrackMaster.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ViewModelBase _currentPage = new SessionsViewModel();

        [ObservableProperty]
        private ListItemTemplate? _selectedListItem;

        partial void OnSelectedListItemChanged(ListItemTemplate? value)
        {
            if (value is null) return;

            var instance = Activator.CreateInstance(value.ModelType);
            if (instance is null) return;
            CurrentPage = (ViewModelBase)instance;
        }
        public ObservableCollection<ListItemTemplate> Items { get; } = new()
        {
            new ListItemTemplate(typeof(SessionsViewModel)),
            new ListItemTemplate(typeof(TelemetryViewModel)),
            new ListItemTemplate(typeof(StrategyViewModel))
        };
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
