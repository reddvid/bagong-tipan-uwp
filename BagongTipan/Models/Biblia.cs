// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using System.Collections.ObjectModel;

namespace BagongTipan.UWP
{
    public class Biblia
    {
        public ObservableCollection<Biblia> Bible { get; set; }
        public string BookTitle { get; set; }
        public string Chapter { get; set; }
        public string Index { get; set; }
        public string Verse { get; set; }
    }

}
