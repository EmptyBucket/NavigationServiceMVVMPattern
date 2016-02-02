using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace NavigationServiceMVVM.Model.NavigationMVVM
{
    public sealed class NavigationServiceSingletonMvvm : INavigationService
    {
        private NavigationService _navigationService;
        public NavigationService NavigationService
        {
            set
            {
                if (Service._navigationService != null)
                    Service._navigationService.Navigated -= NavigationServiceOnNavigated;
                Service._navigationService = value;
                Service._navigationService.Navigated += NavigationServiceOnNavigated;
            }
        }

        public NavigationServiceSingletonMvvm Configure(string key, Page page, ViewModelBase viewModel)
        {
            _pages[key] = new DataPage(page, viewModel);
            return this;
        }

        private static void NavigationServiceOnNavigated(object sender, NavigationEventArgs navigationEventArgs)
        {
            var page = navigationEventArgs.Content as Page;
            if (page != null)
                page.DataContext = navigationEventArgs.ExtraData;
        }

        private readonly List<string> _historic;

        public void NavigateTo(string key) => NavigateTo(key, null);

        public void NavigateTo(string key, object parameter)
        {
            if (Service._navigationService == null || !_pages.ContainsKey(key))
                return;
            Parameter = parameter;
            _historic.Add(key);
            var pageData = _pages[key];
            Service._navigationService.Navigate(pageData.PageInstance, pageData.ViewModel);
        }

        public string CurrentPageKey => _historic.Last();

        public object Parameter { get; private set; }

        public void GoBack()
        {
            if (_historic.Count <= 1) return;
            _historic.RemoveAt(_historic.Count - 1);
            NavigateTo(_historic.Last());
        }

        private static volatile NavigationServiceSingletonMvvm _instance;
        private static readonly object SyncRoot = new object();
        private readonly Dictionary<string, DataPage> _pages;

        private NavigationServiceSingletonMvvm()
        {
            _historic = new List<string>();
            _pages = new Dictionary<string, DataPage>();
        }

        public static NavigationServiceSingletonMvvm Service
        {
            get 
            {
                if (_instance != null) return _instance;
                lock (SyncRoot)
                if (_instance == null) _instance = new NavigationServiceSingletonMvvm();
                return _instance;
            }
        }
    }
}
