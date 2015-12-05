using System;
using MetroIoc;
using Pancakes.ViewModel;

namespace Pancakes
{
    public class ViewModelLocator
    {
        private readonly Lazy<IContainer> _container = new Lazy<IContainer>(IoC.BuildContainer);

        public IContainer Container
        {
            get { return _container.Value; }
        }

        public MainViewModel MainViewModel
        {
            get { return Container.Resolve<MainViewModel>(); }
        }
    }
}