using System.Windows;
using NavigationServiceMVVM.Model.NavigationMVVM;
using NavigationServiceMVVM.ViewModel;
using NavigationServiceMVVM.Views;

namespace NavigationServiceMVVM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            NavigationServiceMvvm.Service.NavigationService = MainFrame.NavigationService;
            var navigationService = NavigationServiceMvvm.Service;
            navigationService
                .Configure(ViewModelLocator.FirstPage, new Page1(), new ViewModelOne(navigationService))
                .Configure(ViewModelLocator.SecordPages, new Page2(), new ViewModelTwo(navigationService));
            NavigationServiceMvvm.Service.NavigateTo(ViewModelLocator.FirstPage);
        }
    }
}