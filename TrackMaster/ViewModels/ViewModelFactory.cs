using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMaster.ViewModels
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewModelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ViewModelBase CreateViewModel(Type viewModelType)
        {
            var viewModel = _serviceProvider.GetService(viewModelType);

            if (viewModel == null)
                throw new ArgumentException($"ViewModel {viewModelType.Name} no registrado en el contenedor");
            return (ViewModelBase)viewModel;
        }
    }
}
