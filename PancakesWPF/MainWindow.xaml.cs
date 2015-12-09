using System.Windows;
using System.Windows.Input;
using PancakesWPF.ViewModel;

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
    }
}