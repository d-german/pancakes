using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using PancakesWPF.ViewModel;
using System.Linq;

namespace PancakesWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }
        

        private void Ui_NumPancakes_OnKeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void getDirections(ref System.Text.StringBuilder buffer)
        {
            buffer.AppendLine("First mix wet ingredients well.");
            buffer.AppendLine("Next stir in the dry ingredients just until everything is wet. Don't over mix!");
            buffer.AppendLine(
                "It should be slightly lumpy. You can let it sit for a few minutes, and the batter should expand a little.");
            buffer.AppendLine(
                "Use a griddle set to 325 - 350 degrees or a skillet set to medium heat.");
            buffer.AppendLine(
                "The batter is a little thick so after putting some on the griddle, use a spoon to flatten out the batter.");
            buffer.AppendLine("Cook about 2 minutes on each side or until they look how you want.");
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var buffer = new StringBuilder();
            buffer.AppendLine("Number of Pancakes: " + ui_NumPancakes.Text);
            buffer.AppendLine(String.Empty);
            foreach (var item in ui_ListBox.Items)
            {
                buffer.AppendLine(item.ToString());
            }
            buffer.AppendLine(String.Empty);
            getDirections(ref buffer);
            var text = buffer.ToString();

            Clipboard.SetText(text);
          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var buffer = new StringBuilder();
            getDirections(ref buffer);
            ui_Label.Content = buffer.ToString();


        }
    }
}