using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BagongTipan.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Get app version
            #region APP VERSION
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            // Set app version to textblock
            tb_version.Text = "version " + string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            #endregion

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
            string _mytext = "Tagalog Bible (Ang Bagong Tipan)";
            request.Data.SetText(_mytext);
        }

        private async void hb_moreapps_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store:Publisher?name=Red David"));
        }

        private async void hb_feedback_Click(object sender, RoutedEventArgs e)
        {
            // Send feedback
            if (Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.IsSupported())
            {
                // Launch feedback
                var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
                await launcher.LaunchAsync();
            }
            else
            {
                // Get device detail
                DeviceInfo _di = new DeviceInfo();

                // via email
                EmailMessage emailMessage = new EmailMessage();
                emailMessage.To.Add(new EmailRecipient("redappsupport@outlook.com"));
                emailMessage.Body = "Tagalog Bible (Ang Bagong Tipan)\n" + _di.appversion + "\n" + _di.devname + "\n" + _di.devmanufacturer + " " + _di.devmodel + "\n" + _di.sysfam + " " + _di.sysversion + " " + _di.sysarch + "\n\n\n";
                emailMessage.Subject = "[FEEDBACK] Tagalog Bible (Ang Bagong Tipan)";
                await EmailManager.ShowComposeNewEmailAsync(emailMessage);
            }
        }

        private async void hb_rate_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(string.Format("ms-windows-store:REVIEW?PFN={0}", Windows.ApplicationModel.Package.Current.Id.FamilyName)));
        }

        private void hb_share_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private async void hb_fb_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("http://www.facebook.com/reddvidapps/"));
        }

        private async void hb_twitter_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("http://www.twitter.com/reddvidapps"));
        }

        private async void hb_donate_Click(object sender, RoutedEventArgs e)
        {
            // Paypal
            await Launcher.LaunchUriAsync(new Uri("https://paypal.me/reddvid/199"));
        }
    }
}
