using MetroIoc;
using Pancakes.ViewModel;

namespace Pancakes
{
    public class IoC
    {
        public static IContainer BuildContainer()
        {
            var container = new MetroContainer();
            container.RegisterInstance(container);
            container.RegisterInstance<IContainer>(container);

            container.Register<MainViewModel>(registration: new Singleton() /*new Transient()*/ );


            return container;
        }
    }
}