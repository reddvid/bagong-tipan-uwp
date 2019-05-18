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

            SelectedBook = Books[1];
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

        private string _selectedChapter;

        public string SelectedChapter
        {
            get => _selectedChapter;
            set
            {
                Set(ref _selectedChapter, value);
                LoadVerses();
                CheckButtons();
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

        private void LoadChapters()
        {
            Chapters.Clear();

            int index = Books.IndexOf(Books.Where(p => p.Equals(SelectedBook)).FirstOrDefault());
            int count = ChapterCount[index];
            for (int c = 1; c <= count; c++)
            {
                Chapters.Add(c.ToString());
            }

            SelectedChapter = Chapters[0];
        }

        private void LoadVerses()
        {
            Contents.Clear();

            int selectedChapterIndex = Chapters.IndexOf(Chapters.Where(c => c.Equals(SelectedChapter)).FirstOrDefault()) + 1;
            foreach (var verse in BibleData.BibliaBiblia.Where(bookName => bookName.Libro == SelectedBook).Where(chapter => chapter.Kabanata == selectedChapterIndex))  //BibleData.Bible.Where(x => x.BookTitle == (SelectedBook)).Where(i => i.Chapter == selectedChapterIndex.ToString()))
            {
                Contents.Add(verse);
            }
        }

        //public bool IsPreviousButtonEnabled => !IsFirstChapter;

        //public bool IsNextButtonEnabled => !IsLastChapter;

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

        private Biblia BibleData;

        public void LoadData()
        {
            BibleData = null;
            string json = null;

            json = File.ReadAllText(@"DataBank.json");

            BibleData = JsonConvert.DeserializeObject<Biblia>(json);
        }
    }
}
