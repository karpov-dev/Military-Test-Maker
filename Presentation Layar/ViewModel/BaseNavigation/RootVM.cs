using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation_Layar.ViewModel.BaseNavigation
{
    class RootVM : ModelPropertyChanged
    {
        private object _currentVM;

        public object CurrentVM
        {
            get
            {
                if ( _currentVM == null )
                {
                    _currentVM = new StartPageVM(this, null);
                    CurrentVM = _currentVM;
                }

                if ( _currentVM == this )
                {
                    System.Windows.Application.Current.Shutdown();
                }

                return _currentVM;
            }
            set
            {
                _currentVM = value;
                OnPropertyChanged("CurrentVM");
            }
        }
    }
}
