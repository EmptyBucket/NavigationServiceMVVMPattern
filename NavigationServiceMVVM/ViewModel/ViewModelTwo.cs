using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace NavigationServiceMVVM.ViewModel
{
    public class ViewModelTwo : ViewModelBase
    {
        private readonly INavigationService _navigatoService;
        public ViewModelTwo(INavigationService navigatoService)
        {
            _navigatoService = navigatoService;
            GoToBackPage = new RelayCommand(() =>
            {
                _navigatoService.NavigateTo(ViewModelLocator.FirstPage);
            });
        }

        public RelayCommand GoToBackPage { get; }
    }
}