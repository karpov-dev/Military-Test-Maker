using Presentation_Layar.Model;
using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.BaseNavigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation_Layar.ViewModel.Pages
{
    class PersonalTestingPageResult : ViewModelBase
    {
        public TestSessionInformation Information { get; set; }

        public PersonalTestingPageResult(RootVM root, object owner, TestSessionInformation information) : base(root, owner) 
        {
            if ( information == null ) Root.CurrentVM = Owner;
            Information = information;
        }

        private RelayCommand _goToMenu;
        public RelayCommand GoToMenu => _goToMenu ?? ( _goToMenu = new RelayCommand(obj =>
        {
            Root.CurrentVM = Owner;
        }) );
    }
}
