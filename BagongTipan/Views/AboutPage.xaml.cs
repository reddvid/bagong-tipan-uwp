using BagongTipan.UWP.ViewModels;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Email;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BagongTipan.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        AboutViewModel ViewModel { get; } = new AboutViewModel();
        public AboutPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {           
            // Initiate Share
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;

            base.OnNavigatedTo(e);
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.Properties.Title = "Tagalog Bible (Ang Bagong Tipan)";
            request.Data.Properties.Description = "Share Tagalog Bible (Ang Bagong Tipan)";
            request.Data.Properties.ApplicationName = "Tagalog Bible (Ang Bagong Tipan)";

            request.Data.SetWebLink(new Uri("http://bit.ly/2jo8VyD"));
            request.Data.SetText("Tagalog Bible (Ang Bagong Tipan)");
        }
        
        private void HyperBtnShare_Click(object sender, RoutedEventArgs e)  
        {
            DataTransferManager.ShowShareUI();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mainPage = MainPage.Current as Page;

            ViewModel.SelectedTheme = (sender as ComboBox).SelectedItem as string;

            switch (ViewModel.SelectedTheme)
            {
                case "Madilim":
                    RememberThemeSetting("Madilim");
                    break;

                case "Maliwanag":
                    RememberThemeSetting("Maliwanag");
                    break;
            }
        }

        private void RememberThemeSetting(string theme)
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            var container = roamingSettings.CreateContainer("settings", ApplicationDataCreateDisposition.Always);

            // Create the key and save the theme setting
            string myKey = "apptheme";

            if (!roamingSettings.Containers["settings"].Values.ContainsKey(myKey))
            {
                roamingSettings.Containers["settings"].Values.Add(myKey.ToString(), theme);
            }
            else
            {
                // Replace the key
                roamingSettings.Containers["settings"].Values.Remove(myKey.ToString());
                roamingSettings.Containers["settings"].Values.Add(myKey.ToString(), theme);
            }
        }
    }
}
