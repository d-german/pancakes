using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PancakeModel;
using PancakesWPF.Model;

namespace PancakesWPF.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private const string Recipe =
            @"First mix wet ingredients well (I use a blender at high speed for about 30 seconds) it will be bubbly.\r\n Next stir in the dry ingredients just until everything is wet. Don't over mix! \r\nIt should be slightly lumpy. You can let it sit for a few minutes and the batter should expand a little. I always use a griddle set to about 325 - 350 degrees but a skillet at medium heat should work?  You might need a non-stick spray. The batter is a little thick so after I put some on the griddle I use my spoon to flatten out the batter. I cook them 2 minutes on each side or until they look how you want.";

        private readonly List<RecipeItemViewModel> _recipeItems;
        private int _numPancakes = 6;

        private string _welcomeTitle = string.Empty;


        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    WelcomeTitle = /*item.Title*/ "My Name Is Damon";
                });

            Eggs = new RecipeItemViewModel {Label = "Eggs:      "};
            Buttermilk = new RecipeItemViewModel {Label = "Buttermilk:"};
            Oil = new RecipeItemViewModel {Label = "Cooking oil:"};
            Vanilla = new RecipeItemViewModel {Label = "Vanilla:"};
            Flour = new RecipeItemViewModel {Label = "All purpose flour:"};
            BakingSoda = new RecipeItemViewModel {Label = "Baking soda:"};
            BakingPowder = new RecipeItemViewModel {Label = "Baking powder:"};
            Sugar = new RecipeItemViewModel {Label = "Granulated sugar:"};

            _recipeItems = new List<RecipeItemViewModel>
            {
                Eggs,
                Buttermilk,
                Oil,
                Vanilla,
                Flour,
                BakingSoda,
                BakingPowder,
                Sugar
            };

            RecpieItems = new ObservableCollection<RecipeItemViewModel>(_recipeItems);


            OnNumPancakesChanged(_numPancakes);
        }

        /// <summary>
        ///     Gets the WelcomeTitle property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string WelcomeTitle
        {
            get { return _welcomeTitle; }

            set
            {
                if (_welcomeTitle == value)
                {
                    return;
                }

                _welcomeTitle = value;
                RaisePropertyChanged();
            }
        }

        public RecipeItemViewModel Eggs { get; set; }
        public RecipeItemViewModel Buttermilk { get; set; }
        public RecipeItemViewModel Oil { get; set; }
        public RecipeItemViewModel Vanilla { get; set; }
        public RecipeItemViewModel Flour { get; set; }
        public RecipeItemViewModel BakingPowder { get; set; }
        public RecipeItemViewModel BakingSoda { get; set; }
        public RecipeItemViewModel Sugar { get; set; }
        public ObservableCollection<RecipeItemViewModel> RecpieItems { get; set; }

       

        public int NumPancakes
        {
            get { return _numPancakes; }

            set
            {
                if (_numPancakes == value)
                {
                    return;
                }

                _numPancakes = value;

                if (_numPancakes < 6)
                {
                    _numPancakes = 6;
                }

                RaisePropertyChanged();

                OnNumPancakesChanged(_numPancakes);
            }
        }

        private int GetContentWith()
        {
            int length = _recipeItems.OrderByDescending(s => s.ToString().Length).First().ToString().Length;
            return length*100;
        }

        public void OnNumPancakesChanged(int numPancakes)
        {
            _numPancakes = numPancakes;
            var rec = new ButtermilkPancakeRecipe(_numPancakes);
            Buttermilk.Amount = rec.GetButtermilkAmount();
            Eggs.Amount = rec.GetEggsAmount();
            Oil.Amount = rec.GetOilAmount();
            Vanilla.Amount = rec.GetBakingPowderAmount();
            BakingPowder.Amount = rec.GetBakingPowderAmount();
            BakingSoda.Amount = rec.GetBakingSodaAmount();
            Sugar.Amount = rec.GetSugerAmount();
            Flour.Amount = rec.GetFlourAmount();

            RecpieItems.Clear();
            
            foreach (var item in _recipeItems)
            {
                RecpieItems.Add(item);
            }
        }
    }
}