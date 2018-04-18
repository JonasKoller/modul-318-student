using PropertyChanged;
using SwissTransport;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissTransportGUI.viewmodel
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    class AbfahrtstafelViewModel
    {
        public AbfahrtstafelViewModel()
        {
            LocationSearchPreviewItems = new ObservableCollection<string>();
            Connections = new ObservableCollection<StationBoardConnection>();
        }

        public string LocationSearchString { get; set; }

        public ObservableCollection<string> LocationSearchPreviewItems { get; set; }

        public ObservableCollection<StationBoardConnection> Connections { get; set; }
    }
}
