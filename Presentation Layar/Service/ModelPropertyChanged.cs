using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Presentation_Layar.Service
{
    class ModelPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if ( PropertyChanged != null )
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
