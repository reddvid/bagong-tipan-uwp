using BagongTipan.UWP.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        MainViewModel ViewModel { get; } = new MainViewModel();
               
        string _sharetext;

        public MainPage()
        {
            this.InitializeComponent();

            // Read Json File
            //string json = File.ReadAllText(@"DataBank.json");
            //obj = JsonConvert.DeserializeObject<RootObject>(json);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Read Json File
            //string json = File.ReadAllText(@"DataBank.json");
            

            // Initiate Share
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;

            base.OnNavigatedTo(e);
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;

            request.Data.Properties.Title = "Tagalog Bible (Ang Bagong Tipan)";
            request.Data.Properties.Description = "Tagalog Bible (Ang Bagong Tipan)";
            request.Data.Properties.ApplicationName = "Tagalog Bible (Ang Bagong Tipan)";
            request.Data.SetWebLink(new Uri("http://bit.ly/2jo8VyD"));
            request.Data.SetText(_sharetext);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested -= DataTransferManager_DataRequested;

            base.OnNavigatedFrom(e);
        }

        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            sv_main.IsPaneOpen = !sv_main.IsPaneOpen;
        }

        private void lv_books_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var item = lv_books.SelectedItem as string;  // Get selected item

            //tb_title.Text = tb_btnlabel.Text = item; // Set title

            //var chapters = new List<string>();
            //chapters.Clear();

            //for (int c = 1; c <= perchaptercount[lv_books.SelectedIndex]; c++)
            //{
            //    chapters.Add(c.ToString());
            //}

            //cb_chapterselection.ItemsSource = chapters;

            //if (cb_chapterselection.Items != null)
            //    cb_chapterselection.SelectedIndex = 0;
        }

        private void lv_books_Loaded(object sender, RoutedEventArgs e)
        {
            //if ((sender as ListView).Items != null)
            //    (sender as ListView).SelectedIndex = 0;
        }

        private void cb_chapterselection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //// Get data from json databank and set it to itemsource of listview
            //var itemsource = new List<Biblia>();
            //foreach (var item in obj.Biblia.Where(h => h.libro == (lv_books.SelectedItem as string)).Where(i => i.kabanata == (cb_chapterselection.SelectedIndex + 1).ToString()))
            //{
            //    itemsource.Add(item);
            //}
            //csv2.Source = itemsource;

            //// Update header            
            //if (cb_chapterselection.SelectedItem != null)
            //    tb_kabanata.Text = "Kabanata " + cb_chapterselection.SelectedItem.ToString();

            //// Check buttons
            //CheckPageButtons();
        }

        private void CheckPageButtons()
        {
            //btn_next.IsEnabled = (cb_chapterselection.SelectedIndex < perchaptercount[lv_books.SelectedIndex] - 1);
            //btn_prev.IsEnabled = (cb_chapterselection.SelectedIndex > 0);
        }

        private void lv_verselist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lv_verselist_Loaded(object sender, RoutedEventArgs e)
        {
            // Update header            
            //if (cb_chapterselection.Items != null)
            //    tb_kabanata.Text = "Kabanata " + cb_chapterselection.SelectedItem.ToString();
        }

        private void btn_prev_Click(object sender, RoutedEventArgs e)
        {
            //cb_chapterselection.SelectedIndex = cb_chapterselection.SelectedIndex - 1;
        }

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
           // cb_chapterselection.SelectedIndex = cb_chapterselection.SelectedIndex + 1;
        }

        private void btn_share_Click(object sender, RoutedEventArgs e)
        {
            //// Initialize verse to share
            //// Bitly url : http://bit.ly/2jo8VyD
            //if (lv_verselist.SelectedItems.Count != 0)
            //{
            //    var selected = lv_verselist.SelectedItem as Biblia;
            //    string[] tobecombined = { selected.libro, selected.kabanata + ":" + selected.index, selected.verse, "\nhttp://bit.ly/2jo8VyD" };

            //    _sharetext = String.Join(" ", tobecombined);
            //}
            //else
            //{
            //    _sharetext = "I-download ang Tagalog Bible (Ang Bagong Tipan) sa iyong Windows 10 device.";
            //}

            //DataTransferManager.ShowShareUI();
        }

        private void g_verseitem_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            // Show where was clicked
            FrameworkElement senderElement = sender as FrameworkElement;
            var tappedItem = (UIElement)e.OriginalSource;

            // Set highlight
            lv_verselist.SelectedIndex = lv_verselist.Items.IndexOf((senderElement).DataContext);

            var attachedFlyout = (MenuFlyout)FlyoutBase.GetAttachedFlyout(senderElement);
            attachedFlyout.ShowAt(tappedItem, e.GetPosition(tappedItem));
        }

        #region FLYOUT METHODS
        private void btn_copyverse_Click(object sender, RoutedEventArgs e)
        {
            //DataPackage dataPackage = new DataPackage();
            //if (lv_verselist.SelectedItems.Count != 0)
            //{
            //    var selected = lv_verselist.SelectedItem as Biblia;
            //    string[] tobecombined = { selected.libro, selected.kabanata + ":" + selected.index, selected.verse, "\nhttp://bit.ly/2jo8VyD" };
            //    dataPackage.SetText(String.Join(" ", tobecombined));
            //    Clipboard.SetContent(dataPackage);

            //    // Show in-app notification
            //    int duration = 3000;
            //    iapn_notify.Show(String.Join(" ", tobecombined), duration);
            //}           
        }
        #endregion

        private void btn_shareverse_Click(object sender, RoutedEventArgs e)
        {
            //if (lv_verselist.SelectedItems.Count != 0)
            //{
            //    var selected = lv_verselist.SelectedItem as Biblia;
            //    string[] tobecombined = { selected.libro, selected.kabanata + ":" + selected.index, selected.verse, "\nhttp://bit.ly/2jo8VyD" };
            //    _sharetext = String.Join(" ", tobecombined);               
            //}
            //DataTransferManager.ShowShareUI();
        }

        private void btn_bookmark_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_about_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            //ViewModel.SelectedBook = (sender as ListView).Items.FirstOrDefault() as string;
        }

        Compositor _compositor = Window.Current.Compositor;
        SpringVector3NaturalMotionAnimation _springAnimation;

        private void CreateOrUpdateSpringAnimation(float finalValue)
        {
            if (_springAnimation == null)
            {
                _springAnimation = _compositor.CreateSpringVector3Animation();
                _springAnimation.Target = "Scale";
            }

            _springAnimation.FinalValue = new Vector3(finalValue);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Animate
            FadeIn.Begin();
            //Storyboard storyboard = new Storyboard();
           

            //TxbTitle.RenderTransform = new ScaleTransform { ScaleX = 0, ScaleY = 0 };
            //DoubleAnimation animateScaleX = CreateDoubleAnimation(TxbTitle.RenderTransform, 0, 1, "(ScaleTransform.ScaleX)", 0.2);
            //storyboard.Children.Add(animateScaleX);
            //DoubleAnimation animateScaleY = CreateDoubleAnimation(TxbTitle.RenderTransform, 0, 1, "(ScaleTransform.ScaleY)", 0.2);
            //storyboard.Children.Add(animateScaleY);

            //storyboard.Begin();

            //CreateOrUpdateSpringAnimation(0.0f);

            //TxbTitle.StartAnimation(_springAnimation);

            //CreateOrUpdateSpringAnimation(1.0f);

            //TxbTitle.StartAnimation(_springAnimation);
        }

        private static DoubleAnimation CreateDoubleAnimation(DependencyObject frameworkElement, double fromX, double toX, string propertyToAnimate, Double interval)
        {
            DoubleAnimation animation = new DoubleAnimation();
            Storyboard.SetTarget(animation, frameworkElement);
            Storyboard.SetTargetProperty(animation, propertyToAnimate);
            animation.From = fromX;
            animation.To = toX;
            animation.Duration = TimeSpan.FromSeconds(interval);
            return animation;
        }
    }

}
