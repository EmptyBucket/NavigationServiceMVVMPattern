using System.Windows.Controls;
using GalaSoft.MvvmLight;

namespace NavigationServiceMVVM.Model.NavigationMVVM
{
    public class DataPage
    {
        public DataPage(Page pageInstance, ViewModelBase viewModel)
        {
            PageInstance = pageInstance;
            ViewModel = viewModel;
        }

        public Page PageInstance { get; }
        public ViewModelBase ViewModel { get; }
    }
}