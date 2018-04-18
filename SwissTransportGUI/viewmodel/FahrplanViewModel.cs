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
    class FahrplanViewModel
    {
        public FahrplanViewModel()
        {
            FromSearchPreviewItems = new ObservableCollection<string>();
            ToSearchPreviewItems = new ObservableCollection<string>();
            Connections = new ObservableCollection<DisplayConnection>();
        }

        public string FromSearchString { get; set; }

        public string ToSearchString { get; set; }

        public ObservableCollection<string> FromSearchPreviewItems { get; set; }

        public ObservableCollection<string> ToSearchPreviewItems { get; set; }

        public ObservableCollection<DisplayConnection> Connections { get; set; }
    }
}
