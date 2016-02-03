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
                    .ConfigurePage<Page1>(ViewModelLocator.FirstPage)
                    .WithViewModelType<ViewModelOne>()
                    .WithViewModelArgs(new[] {navigationService});
                navigationService
                    .ConfigurePage<Page2>(ViewModelLocator.SecordPages)
                    .WithViewModelType<ViewModelTwo>()
                    .WithViewModelArgs(new[] {navigationService});
                navigationService.NavigateTo(ViewModelLocator.FirstPage);
            };
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
    }
}