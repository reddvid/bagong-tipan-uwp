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
            }
        }

        public ObservableCollection<string> Chapters { get; } = new ObservableCollection<string>();
        public ObservableCollection<BibliaElement> Contents { get; } = new ObservableCollection<BibliaElement>();

        private void LoadChapters()
        {
            Chapters.Clear();

            int count = ChapterCount[Books.IndexOf(Books.Where(p => p.Equals(SelectedBook)).FirstOrDefault())];
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
            
            //// Update header            
            //if (cb_chapterselection.SelectedItem != null)
            //    tb_kabanata.Text = "Kabanata " + cb_chapterselection.SelectedItem.ToString();

            //// Check buttons
            //CheckPageButtons();
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
