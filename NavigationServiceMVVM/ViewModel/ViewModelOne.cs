using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace NavigationServiceMVVM.ViewModel
{
    public class ViewModelOne : ViewModelBase
    {
        public RelayCommand GoNextPage { get; }
        private readonly INavigationService _navigationService;
        public ViewModelOne(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoNextPage = new RelayCommand(() =>
            {
                _navigationService.NavigateTo(ViewModelLocator.SecordPages);
            });
        }
    }
}