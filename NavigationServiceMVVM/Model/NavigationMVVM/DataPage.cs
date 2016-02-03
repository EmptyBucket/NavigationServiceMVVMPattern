using System;
using System.Collections.Generic;

namespace NavigationServiceMVVM.Model.NavigationMVVM
{
    public class DataPage
    {
        public DataPage(Type pageType)
        {
            PageType = pageType;
        }

        public DataPage WithViewModelType<T1>()
        {
            ViewModelType = typeof (T1);
            return this;
        }

        public DataPage WithPageArgs(IReadOnlyCollection<object> pageArgs)
        {
            PageArgs = pageArgs;
            return this;
        }

        public DataPage WithViewModelArgs(IReadOnlyCollection<object> viewModelArgs)
        {
            ViewModelArgs = viewModelArgs;
            return this;
        }

        public IReadOnlyCollection<object> ViewModelArgs { get; private set; }
        public IReadOnlyCollection<object> PageArgs { get; private set; }
        public Type PageType { get; private set; }
        public Type ViewModelType { get; private set; }
    }
}