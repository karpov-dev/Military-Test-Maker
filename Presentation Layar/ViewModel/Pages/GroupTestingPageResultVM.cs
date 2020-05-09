using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.BaseNavigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation_Layar.ViewModel.Pages
{
    class GroupTestingPageResultVM : ViewModelBase 
    {
        public GroupTestingPageResultVM(RootVM root, object owner) : base(root, owner) { }

        private RelayCommand _goToMenu;
        public RelayCommand GoToMenu => _goToMenu ?? ( _goToMenu = new RelayCommand(obj =>
        {
            Root.CurrentVM = Owner;
        }));
    }
}
