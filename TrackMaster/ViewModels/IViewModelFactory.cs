using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMaster.ViewModels
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(Type viewModelType);
    }
}
