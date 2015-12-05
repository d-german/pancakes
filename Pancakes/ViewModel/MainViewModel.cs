using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using Pancakes.Messages;
using Pancakes.Model;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace Pancakes.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const string Recpie =
            @"First mix wet ingredients well (I use a blender at high speed for about 30 seconds) it will be bubbly. Next stir in the dry ingredients just until everything is wet. Don't over mix! it should be slightly lumpy. You can let it sit for a few minutes and the batter should expand a little. I always use a griddle set to about 325 - 350 degrees but a skillet at medium heat should work?  You might need a non-stick spray. The batter is a little thick so after I put some on the griddle I use my spoon to flatten out the batter. I cook them 2 minutes on each side or until they look how you want.";

        private int _numPancakes;
        private List<RecipeItemViewModel> _recpieItems;
        private Orientation _selectorOrientation;

        public MainViewModel()
        {
            RegisterMessenger();
            InitRecpieDisplayItems();
        }

        public String SharePayload { get; set; }

        public ObservableCollection<RecipeItemViewModel> RecpieItems { get; set; }

        public string ShareData
        {
            get
            {
                var buf = new StringBuilder();

                buf.AppendLine(string.Format("<p>Number of Pancakes: {0}</p>", NumPancakes));


                foreach (var recpieItem in _recpieItems)
                {
                    buf.AppendLine(string.Format("<p>{0} {1}</p>", recpieItem.Label, recpieItem.Amount));
                }

                buf.AppendLine(string.Format("<p>{0}</p>", Recpie));

                return buf.ToString();
            }
        }

        public int NumPancakes
        {
            get { return _numPancakes; }
            set
            {
                _numPancakes = value;

                RaisePropertyChanged();
            }
        }

        public Orientation SelectorOrientation
        {
            get { return _selectorOrientation; }
            set
            {
                _selectorOrientation = value;

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

        private void InitRecpieDisplayItems()
        {
            _recpieItems = new List<RecipeItemViewModel>();

            Eggs = new RecipeItemViewModel {Label = "Eggs:      "};
            Buttermilk = new RecipeItemViewModel {Label = "Buttermilk:"};
            Oil = new RecipeItemViewModel {Label = "Cooking oil:"};
            Vanilla = new RecipeItemViewModel {Label = "Vanilla:"};
            Flour = new RecipeItemViewModel {Label = "All purpose flour:"};
            BakingSoda = new RecipeItemViewModel {Label = "Baking soda:"};
            BakingPowder = new RecipeItemViewModel {Label = "Baking powder:"};
            Sugar = new RecipeItemViewModel {Label = "Granulated sugar:"};

            _recpieItems.Add(Eggs);
            _recpieItems.Add(Buttermilk);
            _recpieItems.Add(Oil);
            _recpieItems.Add(Vanilla);
            _recpieItems.Add(Flour);
            _recpieItems.Add(BakingSoda);
            _recpieItems.Add(BakingPowder);
            _recpieItems.Add(Sugar);

            RecpieItems = new ObservableCollection<RecipeItemViewModel>(_recpieItems);

            ChangeDisplayOrientation(Orientation.Horizontal);
        }

        public string GetShareMessage()
        {
            var buf = new StringBuilder();

            foreach (var item in RecpieItems)
            {
                buf.Append(item.Label);
            }
            return null;
        }

        private void ChangeDisplayOrientation(Orientation orientation)
        {
            foreach (var item in _recpieItems)
            {
                item.DisplayOrientation = orientation;
            }
        }

        private void RegisterMessenger()
        {
            Messenger.Default.Register<NumPancakesChangedMsg>(this, OnNumPancakesChanged);
            Messenger.Default.Register<ViewStateChangedMsg>(this, OnViewStateChangedMsg);
        }

        private void OnViewStateChangedMsg(ViewStateChangedMsg msg)
        {
            switch (msg.ApplicationViewState)
            {
                case ApplicationViewState.FullScreenPortrait:
                    ChangeDisplayOrientation(Orientation.Horizontal);
                    SelectorOrientation = Orientation.Horizontal;
                    break;

                case ApplicationViewState.FullScreenLandscape:
                    ChangeDisplayOrientation(Orientation.Horizontal);
                    SelectorOrientation = Orientation.Horizontal;
                    break;

                case ApplicationViewState.Snapped:
                    ChangeDisplayOrientation(Orientation.Vertical);
                    SelectorOrientation = Orientation.Vertical;
                    break;
            }
        }

        public void OnNumPancakesChanged(NumPancakesChangedMsg msg)
        {
            _numPancakes = msg.NumPancakes;
            var rec = new ButtermilkPancakeRecipe(_numPancakes);
            Buttermilk.Amount = rec.GetButtermilkAmount();
            Eggs.Amount = rec.GetEggsAmount();
            Oil.Amount = rec.GetOilAmount();
            Vanilla.Amount = rec.GetBakingPowderAmount();
            BakingPowder.Amount = rec.GetBakingPowderAmount();
            BakingSoda.Amount = rec.GetBakingSodaAmount();
            Sugar.Amount = rec.GetSugerAmount();
            Flour.Amount = rec.GetFlourAmount();

            RecpieItems = new ObservableCollection<RecipeItemViewModel>(_recpieItems);
        }
    }
}