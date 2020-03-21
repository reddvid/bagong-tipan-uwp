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
    public class Books
    {
        public static ObservableCollection<string> BooksList
        {
            get
            {
                return new ObservableCollection<string>()
                {
                    "Mateo",
                    "Marcos",
                    "Lucas",
                    "Juan",
                    "Mga Gawa",
                    "Mga Taga-Roma",
                    "1 Mga Taga-Corinto",
                    "2 Mga Taga-Corinto",
                    "Mga Taga-Galacia",
                    "Mga Taga-Efeso",
                    "Mga Taga-Filipos",
                    "Mga Taga-Colosas",
                    "1 Mga Taga-Tesalonica",
                    "2 Mga Taga-Tesalonica",
                    "1 Timoteo",
                    "2 Timoteo",
                    "Tito",
                    "Filemon",
                    "Mga Hebreo",
                    "Santiago",
                    "1 Pedro",
                    "2 Pedro",
                    "1 Juan",
                    "2 Juan",
                    "3 Juan",
                    "Judas",
                    "Pahayag",
                };
            }
        }

        public static string[] Abbreviations
        {
            get
            {
                return new string[] {
                    "Mt", "Mr", "Lk", "Jh", "Ac", "Rm", "1Cr",
                    "2Cr", "Gl", "Ep", "Ph", "Cl", "1Th", "2Th",
                    "1Tm", "2Tm", "Tt", "Pl", "Hb", "Jm", "1Pt", 
                    "2Pt", "1Jh", "2Jh", "3Jh", "Jd", "Rv" 
                };
            }
        }

        public static int[] ChapterCount
        {
            get
            {
                return new int[] {
                    28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22 };
            }
        }



    }
}
