using BagongTipan.UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Composition;
using Windows.UI.Xaml;

namespace BagongTipan.UWP.ViewModels
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<string> Books { get; private set; }
        private int[] ChapterCount { get; set; }

        public MainViewModel()
        {
            LoadData();

            var books = new Books();

            Books = books.BooksList;
            ChapterCount = books.ChapterCount;

            LoadFontSettings();

            LoadBookmarks();
        }

		private Biblia BibleData { get; set; }

		public void LoadData()
		{
            string json = File.ReadAllText(@"DataBank.json");

			BibleData = JsonConvert.DeserializeObject<Biblia>(json);
		}

		private void LoadBookmarks()
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            if (roamingSettings.Containers.ContainsKey("settings") == true)
            {
                var bookmark = roamingSettings.Containers["settings"].Values["bookmark"];
                var chaptermark = roamingSettings.Containers["settings"].Values["chaptermark"];

                if (bookmark != null && chaptermark != null)
                {
                    SelectedBook = (string)bookmark;
                    SelectedChapter = (string)chaptermark;
                }
                else
                {
                    SelectedBook = Books[0];
                }
            }
        }

        private void LoadFontSettings()
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            if (roamingSettings.Containers.ContainsKey("settings") == true)
            {
                var fontsize = roamingSettings.Containers["settings"].Values["fontsize"];
                if (fontsize != null)
                {
                    FontSize = (int)fontsize;
                }
                else
                {
                    FontSize = 14;
                }
            }
        }

        private void RememberSettings()
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            var container = roamingSettings.CreateContainer("settings", ApplicationDataCreateDisposition.Always);

            // Create the key and save the theme setting
            string myKey = "fontsize";

            if (!roamingSettings.Containers["settings"].Values.ContainsKey(myKey))
            {
                roamingSettings.Containers["settings"].Values.Add(myKey.ToString(), FontSize);
            }
            else
            {
                // Replace the key
                roamingSettings.Containers["settings"].Values.Remove(myKey.ToString());
                roamingSettings.Containers["settings"].Values.Add(myKey.ToString(), FontSize);
            }
        }

        private string _selectedBook;

        public string SelectedBook
        {
            get => _selectedBook;
            set
            {
                Set(ref _selectedBook, value);
                LoadChapters();
            }
        }

        private void RememberBookmarkSettings()
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            var container = roamingSettings.CreateContainer("settings", ApplicationDataCreateDisposition.Always);

            // Create the key and save the theme setting
            string myKey = "bookmark";
            string myKey2 = "chaptermark";

            if (!roamingSettings.Containers["settings"].Values.ContainsKey(myKey))
            {
                roamingSettings.Containers["settings"].Values.Add(myKey.ToString(), SelectedBook);
            }
            else
            {
                // Replace the key
                roamingSettings.Containers["settings"].Values.Remove(myKey.ToString());
                roamingSettings.Containers["settings"].Values.Add(myKey.ToString(), SelectedBook);
            }

            if (!roamingSettings.Containers["settings"].Values.ContainsKey(myKey2))
            {
                roamingSettings.Containers["settings"].Values.Add(myKey2.ToString(), SelectedChapter);
            }
            else
            {
                // Replace the key
                roamingSettings.Containers["settings"].Values.Remove(myKey2.ToString());
                roamingSettings.Containers["settings"].Values.Add(myKey2.ToString(), SelectedChapter);
            }
        }

        private string _selectedChapter;

        public string SelectedChapter
        {
            get => _selectedChapter;
            set
            {
                Set(ref _selectedChapter, value);
                LoadVerses();
                CheckButtons();
                RememberBookmarkSettings();
            }
        }

        private void CheckButtons()
        {
            IsLastChapter = IsFirstChapter = false;

            int index = Books.IndexOf(Books.Where(p => p.Equals(SelectedBook)).FirstOrDefault());
            int count = ChapterCount[index];

            if (SelectedChapter == count.ToString())
            {
                IsLastChapter = true;
            }

            if (SelectedChapter == "1")
            {
                IsFirstChapter = true;
            }
        }

        public ObservableCollection<string> Chapters { get; } = new ObservableCollection<string>();
        public ObservableCollection<BibliaElement> Contents { get; } = new ObservableCollection<BibliaElement>();

        private string _stringifiedContents;
        public string StringifiedContents
        {
            get => _stringifiedContents;
            set
            {
                Set(ref _stringifiedContents, value);
            }
        }

        private string _stringifiedContentsColumnTwo;
        public string StringifiedContentsOnColumnTwo
        {
            get => _stringifiedContentsColumnTwo;
            set
            {
                Set(ref _stringifiedContentsColumnTwo, value);
            }
        }

        private int _fontSize;
        public int FontSize
        {
            get => _fontSize;
            set
            {
                Set(ref _fontSize, value);
                IsSmallestFont = (FontSize <= 12) ? true : false;
                IsLargestFont = (FontSize >= 28) ? true : false;
                RememberSettings();
            }
        }

        private int _lines;
        public int VerseLines
        {
            get => _lines;
            set
            {
                Set(ref _lines, value);
            }
        }

        private void LoadChapters()
        {
            Chapters.Clear();

            int index = Books.IndexOf(Books.Where(p => p.Equals(SelectedBook)).FirstOrDefault());
            int count = ChapterCount[index];
            for (int c = 1; c <= count; c++)
            {
                Chapters.Add(c.ToString());
            }

            if (String.IsNullOrEmpty(SelectedChapter)) SelectedChapter = Chapters[0];
        }

        private void LoadVerses()
        {
            Contents.Clear();
            StringifiedContents = StringifiedContentsOnColumnTwo = string.Empty;
            StringifiedContents = StringifiedContentsOnColumnTwo = null;

            int selectedChapterIndex = Chapters.IndexOf(Chapters.Where(c => c.Equals(SelectedChapter)).FirstOrDefault()) + 1;
            //int count = BibleData.BibliaBiblia.Where(bookName => bookName.Libro == SelectedBook).Where(chapter => chapter.Kabanata == selectedChapterIndex).Count();
            var items = new ObservableCollection<BibliaElement>(BibleData.BibliaBiblia.Where(bookName => bookName.Libro == SelectedBook).Where(chapter => chapter.Kabanata == selectedChapterIndex));

            for (int i = 0; i < items.Count(); i++)
            {
                if (i <= (items.Count / 2) + 3)
                {
                    StringifiedContents += $"{Minify(items[i].Index)} {items[i].Verse}\n";
                }
                else
                {
                    StringifiedContentsOnColumnTwo += $"{Minify(items[i].Index)} {items[i].Verse}\n";
                }
            }

            //foreach (var verse in BibleData.BibliaBiblia.Where(bookName => bookName.Libro == SelectedBook).Where(chapter => chapter.Kabanata == selectedChapterIndex))  //BibleData.Bible.Where(x => x.BookTitle == (SelectedBook)).Where(i => i.Chapter == selectedChapterIndex.ToString()))
            //{
            //    Contents.Add(verse);


            //    StringifiedContents += $"{Minify(verse.Index)} {verse.Verse}\n";
            //}

            //VerseLines = Contents.Count() * 3;

        }

        private string Minify(long index)
        {
            return index.ToString().Replace('1', '¹').Replace('2', '²').Replace('3', '³').Replace('4', '⁴').Replace('5', '⁵').Replace('6', '⁶').Replace('7', '⁷').Replace('8', '⁸').Replace('9', '⁹').Replace('0', '⁰');
        }

        //public bool IsPreviousButtonEnabled => !IsFirstChapter;

        //public bool IsNextButtonEnabled => !IsLastChapter;

        public void IncreaseFont()
        {
            FontSize += 2;
        }

        public void DecreaseFont()
        {
            FontSize -= 2;
        }

        public void GoToPreviousChapter()
        {
            int index = Chapters.IndexOf(Chapters.Where(c => c.Equals(SelectedChapter)).FirstOrDefault());
            SelectedChapter = Chapters[index - 1];
        }

        public void GoToNextChapter()
        {
            int index = Chapters.IndexOf(Chapters.Where(c => c.Equals(SelectedChapter)).FirstOrDefault());
            SelectedChapter = Chapters[index + 1];
        }

        private bool _isSmallestFont;

        public bool IsSmallestFont
        {
            get => !_isSmallestFont;
            set
            {
                Set(ref _isSmallestFont, value);
            }
        }

        private bool _isLargestFont;

        public bool IsLargestFont
        {
            get => !_isLargestFont;
            set
            {
                Set(ref _isLargestFont, value);
            }
        }

        private bool _isFirstChapter;

        public bool IsFirstChapter
        {
            get => !_isFirstChapter;
            set
            {
                Set(ref _isFirstChapter, value);
            }
        }

        private bool _isLastChapter;

        public bool IsLastChapter
        {
            get => !_isLastChapter;
            set
            {
                Set(ref _isLastChapter, value);
            }
        }

        
    }
}
