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
            Loaded += (sender, args) =>
            {
                var navigationService = new NavigationServiceMvvm(MainFrame.NavigationService);
                navigationService
                    .Configure(ViewModelLocator.FirstPage, new Page1(), new ViewModelOne(navigationService))
                    .Configure(ViewModelLocator.SecordPages, new Page2(), new ViewModelTwo(navigationService));
                navigationService.NavigateTo(ViewModelLocator.FirstPage);
            };
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}