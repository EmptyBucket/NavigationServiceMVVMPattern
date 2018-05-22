using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Views;

namespace NavigationServiceMVVM.Model.NavigationMVVM
{
    public class NavigationServiceMvvm : INavigationService
    {
        private readonly System.Windows.Navigation.NavigationService _navigationService;

        public DataPage ConfigurePage<T1>(string key) where T1 : FrameworkElement
        {
             _pages[key] = new DataPage(typeof(T1));
            return _pages[key];
        }

        private static void NavigationServiceOnNavigated(object sender, NavigationEventArgs navigationEventArgs)
        {
            var page = navigationEventArgs.Content as FrameworkElement;
            var data = navigationEventArgs.ExtraData;
            if (page != null && data != null)
                page.DataContext = data;
        }

        private readonly List<string> _historic;

        public void NavigateTo(string key) => NavigateTo(key, null);

        public void NavigateTo(string key, object parameter)
        {
            if (!_pages.ContainsKey(key))
                return;
            Parameter = parameter;
            _historic.Add(key);
            var pageData = _pages[key];

            var pageArgs = pageData.PageArgs ?? new object[0];
            var pageArgsTypes = pageArgs.Select(arg => arg.GetType()).ToArray();
            var pageConstructor = pageData.PageType.GetConstructor(pageArgsTypes);
            
            var viewModelArgs = pageData.ViewModelArgs ?? new object[0];
            var viewModelArgsTypes = viewModelArgs.Select(arg => arg.GetType()).ToArray();
            var viewModelConstructor = pageData.ViewModelType.GetConstructor(viewModelArgsTypes);

            if (pageConstructor == null)
                throw new ArgumentException($"Unable to find a constructor page that takes type:{string.Join(", ", pageArgsTypes.Select(arg => arg.Name))}");
            if(viewModelConstructor == null)
                throw new ArgumentException($"Unable to find a constructor viewModel that takes type:{string.Join(", ", viewModelArgsTypes.Select(arg => arg.Name))}");

            var pageInstance = pageConstructor.Invoke(pageArgs.ToArray());
            var viewModelInstance = viewModelConstructor.Invoke(viewModelArgs.ToArray());

            _navigationService.Navigate(pageInstance, viewModelInstance);
        }

        public string CurrentPageKey => _historic.Last();

        public object Parameter { get; private set; }

        public void GoBack()
        {
            if (_historic.Count <= 1) return;
            _historic.RemoveAt(_historic.Count - 1);
            NavigateTo(_historic.Last());
        }

        private readonly Dictionary<string, DataPage> _pages;

        public NavigationServiceMvvm(System.Windows.Navigation.NavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.Navigated += NavigationServiceOnNavigated;
            _historic = new List<string>();
            _pages = new Dictionary<string, DataPage>();
        }
    }
}
