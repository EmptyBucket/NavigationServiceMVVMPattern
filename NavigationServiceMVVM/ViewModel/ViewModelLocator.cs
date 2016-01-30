using NavigationServiceMVVM.Model.NavigationMVVM;
using Ninject;

namespace NavigationServiceMVVM.ViewModel
{
    public class ViewModelLocator
    {
        private static readonly StandardKernel Kernel = new StandardKernel(new CommonModule());

        public MainViewModel Main => Kernel.Get<MainViewModel>();
        public const string SecordPages = nameof(SecordPages);
        public const string FirstPage = nameof(FirstPage);

        public static void Cleanup()
        {
        }
    }
}