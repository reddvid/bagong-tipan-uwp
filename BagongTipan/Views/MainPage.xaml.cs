using BagongTipan.UWP.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Composition;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BagongTipan.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current { get; private set; }
        public MainViewModel ViewModel { get; set; }
        DataRequest request;
        string ShareText { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            ViewModel = new MainViewModel();
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            request = args.Request;
            request.Data.Properties.Title = "Tagalog Bible (Ang Bagong Tipan)";
            request.Data.Properties.Description = "Tagalog Bible (Ang Bagong Tipan)";
            request.Data.Properties.ApplicationName = "Tagalog Bible (Ang Bagong Tipan)";
            request.Data.SetWebLink(new Uri("http://bit.ly/2jo8VyD"));
            request.Data.SetText(ShareText);
        }

        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
        }

        private void VerseItem_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            // Show where was clicked
            FrameworkElement senderElement = sender as FrameworkElement;
            var tappedItem = (UIElement)e.OriginalSource;

            // Set highlight
            //VerseListView.SelectedIndex = VerseListView.Items.IndexOf((senderElement).DataContext);

            var attachedFlyout = (MenuFlyout)FlyoutBase.GetAttachedFlyout(senderElement);
            attachedFlyout.ShowAt(tappedItem, e.GetPosition(tappedItem));
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }
       
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //VerseScroll.ScrollToVerticalOffset(0);
            VerseScroll.ChangeView(0, 0, 1, false);
            //Animate
            FadeIn.Begin();
        }

        private void AppBarToggleButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void IndexListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //VerseScroll.ScrollToVerticalOffset(0);
            VerseScroll.ChangeView(0, 0, 1, false);
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Debug.WriteLine("This happened");
        }
    }
}
