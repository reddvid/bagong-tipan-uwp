using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagongTipan.UWP
{
    class Verses
    {

        public string chindex { get; set; }
        public string vindex { get; set; }
        public string verse { get; set; }

        private ObservableCollection<Verses> _chapters = new ObservableCollection<Verses>();

        public ObservableCollection<Verses> Mateo()
        {
            _chapters.Add(new Verses { chindex = "1", vindex = "1", verse = "test verse" });

            return _chapters;
        }


    }
}
