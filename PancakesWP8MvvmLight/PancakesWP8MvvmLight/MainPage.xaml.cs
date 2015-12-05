using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;

namespace PancakesWP8MvvmLight
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        
        private void Ui_NumPancakes_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Unknown))
            {
                e.Handled = true;
                return;
            }
        }

        private void Sms_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send(new Messages.SendRecpieViaSmsMsg());
        }

        private void Email_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send(new Messages.SendRecpieViaEmailMsg());
        }
    }
}