using System;
using GalaSoft.MvvmLight.Messaging;
using Pancakes.Common;
using Pancakes.Messages;
using Pancakes.ViewModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Pancakes.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : LayoutAwarePage
    {
        public const string MissingTitleError = "Enter a title for what you are sharing and try again.";
        private readonly DataTransferManager _dataTransferManager;

        public MainPage()
        {
            InitializeComponent();
            UiImage.Source = ImageFromRelativePath(this, "IMG_1570.jpg");
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;
        }

        // When share is invoked (by the user or programatically) the event handler we registered will be called to populate the datapackage with the
        // data to be shared.
        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            // Call the scenario specific function to populate the data package with the data to be shared.
            if (!GetShareContent(e.Request)) return;

            //GetShareContent(e.Request);

            // Out of the data package properties, the title is required. If the scenario completed successfully, we need
            // to make sure the title is valid since the sample scenario gets the title from the user.
            if (String.IsNullOrEmpty(e.Request.Data.Properties.Title))
            {
                e.Request.FailWithDisplayText(MissingTitleError);
            }
        }

        // This function is implemented by each scenario to share the content specific to that scenario (text, link, image, etc.).
        private bool GetShareContent(DataRequest request)
        {
            var succeeded = false;

            var vm = DataContext as MainViewModel;

            if (vm == null) return false;

            var dataPackageText = vm.ShareData;

            if (!String.IsNullOrEmpty(dataPackageText))
            {
                var requestData = request.Data;
                requestData.Properties.Title = "Buttermilk Pancakes";
                requestData.Properties.Description = "recipe"; // The description is optional.

                var html = HtmlFormatHelper.CreateHtmlFormat(dataPackageText);
                requestData.SetText(dataPackageText);
                succeeded = true;
            }
            else
            {
                request.FailWithDisplayText("Enter the text you would like to share and try again.");
            }
            return succeeded;
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //Messenger.Default.Send(new SendRecpieViaEmailMsg());
            //OnEmail();
            DataTransferManager.ShowShareUI();
        }

        protected override string DetermineVisualState(ApplicationViewState viewState)
        {
            Messenger.Default.Send(new ViewStateChangedMsg {ApplicationViewState = viewState});
            return base.DetermineVisualState(viewState);
        }

        /// <summary>
        ///     Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">
        ///     Event data that describes how this page was reached.  The Parameter
        ///     property is typically used to configure the page.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public static BitmapImage ImageFromRelativePath(FrameworkElement parent, string path)
        {
            var uri = new Uri(parent.BaseUri, path);
            var result = new BitmapImage {UriSource = uri};
            return result;
        }

        private void RangeBase_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            var slider = sender as Slider;
            if (slider == null) return;

            Messenger.Default.Send(new NumPancakesChangedMsg
                {
                    NumPancakes = (int) slider.Value
                });

            if (textBlock1 == null) return;

            var msg = String.Format("Pancakes per batch: {0}", e.NewValue);
            textBlock1.Text = msg;
        }
    }
}