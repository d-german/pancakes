using System.Runtime.CompilerServices;

namespace PancakesWP8MvvmLight.ViewModel
{
    public class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        protected override void RaisePropertyChanged([CallerMemberName] string propertyName = null)

        {
            base.RaisePropertyChanged(propertyName);
        }
    }
}