using Windows.UI.Xaml.Controls;

namespace Pancakes.ViewModel
{
    public class RecipeItemViewModel : ViewModelBase
    {
        private string _amount;
        private int _amountIndention;
        private Orientation _displayOrientation;
        private string _label;
        private int _labelIndention;


        public override string ToString()
        {
            return Label + " " + Amount;
        }
       

        public RecipeItemViewModel()
        {
            LabelIndent = AmountIndent = 0;
        }



        public int LabelIndent
        {
            get { return _labelIndention; }
            set
            {
                _labelIndention = value;
                RaisePropertyChanged();
            }
        }

        public int AmountIndent
        {
            get { return _amountIndention; }
            set
            {
                _amountIndention = value;

                RaisePropertyChanged();
            }
        }

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;

                RaisePropertyChanged();
            }
        }

        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;

                RaisePropertyChanged();
            }
        }

        public Orientation DisplayOrientation
        {
            get { return _displayOrientation; }
            set
            {
                _displayOrientation = value;

                RaisePropertyChanged();
            }
        }
    }
}