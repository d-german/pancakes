using System.Runtime.CompilerServices;

namespace Pancakes.ViewModel
{
    public class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
// ReSharper disable OptionalParameterHierarchyMismatch
        protected override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
// ReSharper restore OptionalParameterHierarchyMismatch
        {
            base.RaisePropertyChanged(propertyName);
        }
    }
}