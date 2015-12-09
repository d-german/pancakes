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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var buffer = new StringBuilder();
            buffer.AppendLine("Number of Pancakes: " + ui_NumPancakes.Text);
            buffer.AppendLine(String.Empty);
            foreach (var item in ui_ListBox.Items)
            {
                buffer.AppendLine(item.ToString());
            }

            Clipboard.SetText(buffer.ToString());
          
        }
    }
}