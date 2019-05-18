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

        public ObservableCollection<string> Chapters { get; } = new ObservableCollection<string>(); //_chapters;// = new List<string>();

        //public List<string> Chapters
        //{
        //    get => _chapters;
        //    set
        //    {
        //        Set(ref _chapters, value);
        //    }
        //}

        private void LoadChapters()
        {
            Chapters.Clear();

            int count = ChapterCount[Books.IndexOf(Books.Where(p => p.Equals(SelectedBook)).FirstOrDefault())];
            for (int c = 1; c <= count; c++)
            {
                Chapters.Add(c.ToString());
            }
        }

        private void LoadVerses()
        {
            
        }
        
        public void LoadData()
        {
            string json = null;
            json = File.ReadAllText(@"DataBank.json");

            var obj = JsonConvert.DeserializeObject<Biblia>(json);

        }
    }
}
