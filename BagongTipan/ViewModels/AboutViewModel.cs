using BagongTipan.UWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Email;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BagongTipan.UWP.ViewModels
{
    class AboutViewModel : ObservableObject
    {
        public ObservableCollection<string> Themes { get; private set; }

        public AboutViewModel()
        {
            Themes = new ObservableCollection<string>
            {
                "Madilim",
                "Maliwanag"
            };

            var roamingSettings = ApplicationData.Current.RoamingSettings;
            if (roamingSettings.Containers.ContainsKey("settings") == true)
            {
                var theme = roamingSettings.Containers["settings"].Values["apptheme"];
                if (theme != null)
                {
                    SelectedTheme = (string)theme;
                }
            }
        }

        public bool AppNeedsRestart => SelectedTheme != ((MainPage.Current as Page).RequestedTheme).ToString();

        private string _selectedTheme;

        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                Set(ref _selectedTheme, value);
                //RaisePropertyChanged(nameof(AppNeedsRestart));
            }
        }

        public string AppVersion => GetAppVersion();

        private string GetAppVersion()
        {
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            // Set app version to textblock
            return String.Format("version {0}.{1}.{2}", version.Major, version.Minor, version.Build);
        }

        public async void GoToStore() => await Launcher.LaunchUriAsync(new Uri("ms-windows-store:Publisher?name=Red David"));

        public async void SendFeedback()
        {
            // Send feedback
            if (!Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.IsSupported())
            {
                // Get device detail
                var deviceInfo = new DeviceInfo();

                // via email
                var emailMessage = new EmailMessage();
                emailMessage.To.Add(new EmailRecipient("redappsupport@outlook.com"));
                emailMessage.Body = "Ang Bagong Tipan (Tagalog Bible)\n" + deviceInfo.appversion + "\n" + deviceInfo.devname + "\n" + deviceInfo.devmanufacturer + " " + deviceInfo.devmodel + "\n" + deviceInfo.sysfam + " " + deviceInfo.sysversion + " " + deviceInfo.sysarch + "\n\n\n";
                emailMessage.Subject = "[FEEDBACK] Ang Bagong Tipan (Tagalog Bible)";
                await EmailManager.ShowComposeNewEmailAsync(emailMessage);
            }
            else
            {
                // Launch feedback
                var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
                await launcher.LaunchAsync();
            }
        }

        public async void RateApp() => await Launcher.LaunchUriAsync(new Uri(string.Format("ms-windows-store:review?productid={0}", "9p4n369p108d")));

        public async void OpenFacebook() => await Launcher.LaunchUriAsync(new Uri("http://www.facebook.com/reddvidapps/"));

        public async void OpenTwitter() => await Launcher.LaunchUriAsync(new Uri("http://www.twitter.com/reddvid"));

        public async void Donate() => await Launcher.LaunchUriAsync(new Uri("https://paypal.me/reddvid/49"));
    }
}
