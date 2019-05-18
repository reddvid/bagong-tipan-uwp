using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BagongTipan.UWP.Models
{
    class Books : INotifyPropertyChanged
    {
        private ObservableCollection<string> _booksList = new ObservableCollection<string>();

        public ObservableCollection<string> BooksList
        {
            get
            {
                {
                    _booksList.Add("Mateo");
                    _booksList.Add("Marcos");
                    _booksList.Add("Lucas");
                    _booksList.Add("Juan");
                    _booksList.Add("Mga Gawa");
                    _booksList.Add("Mga Taga-Roma");
                    _booksList.Add("1 Mga Taga-Corinto");
                    _booksList.Add("2 Mga Taga-Corinto");
                    _booksList.Add("Mga Taga-Galacia");
                    _booksList.Add("Mga Taga-Efeso");
                    _booksList.Add("Mga Taga-Filipos");
                    _booksList.Add("Mga Taga-Colosas");
                    _booksList.Add("1 Mga Taga-Tesalonica");
                    _booksList.Add("2 Mga Taga-Tesalonica");
                    _booksList.Add("1 Timoteo");
                    _booksList.Add("2 Timoteo");
                    _booksList.Add("Tito");
                    _booksList.Add("Filemon");
                    _booksList.Add("Mga Hebreo");
                    _booksList.Add("Santiago");
                    _booksList.Add("1 Pedro");
                    _booksList.Add("2 Pedro");
                    _booksList.Add("1 Juan");
                    _booksList.Add("2 Juan");
                    _booksList.Add("3 Juan");
                    _booksList.Add("Judas");
                    _booksList.Add("Pahayag");

                    return _booksList;
                }
            }
        }

        private string[] _abbrevs = new string[0];

        public string[] Abbreviations
        {
            get
            {
                {
                    _abbrevs = new string[] { "Mt", "Mr", "Lk", "Jh", "Ac", "Rm", "1Cr",
                    "2Cr", "Gl", "Ep", "Ph", "Cl", "1Th", "2Th", "1Tm", "2Tm", "Tt",
                    "Pl", "Hb", "Jm", "1Pt", "2Pt", "1Jh", "2Jh", "3Jh", "Jd", "Rv" };

                    return _abbrevs;
                }
            }
        }

        private int[] _chapterCount = new int[0];

        public int[] ChapterCount
        {
            get
            {
                {
                    _chapterCount = new int[] { 28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22 };
                    return _chapterCount;
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
