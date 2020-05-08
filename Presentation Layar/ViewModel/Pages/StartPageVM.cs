﻿using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.BaseNavigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation_Layar.ViewModel.Pages
{
    class StartPageVM : ViewModelBase
    {
        #region Constructors
        public StartPageVM(RootVM root, object owner) : base(root, owner) { }
        #endregion

        #region Relay Commands
        private RelayCommand _goToTesting;
        public RelayCommand GoToTesting => _goToTesting ?? ( _goToTesting = new RelayCommand(obj =>
        {

        }));

        private RelayCommand _goToLearning;
        public RelayCommand GoToLearning => _goToLearning ?? ( _goToLearning = new RelayCommand(obj =>
        {

        }));

        private RelayCommand _goToTestEditor;
        public RelayCommand GoToTestEditor => _goToTestEditor ?? ( _goToTestEditor = new RelayCommand(obj =>
        {
            Root.CurrentVM = new TestEditorPageVM(Root, this);
        }));

        private RelayCommand _exitApplication;
        public RelayCommand ExitApplication => _exitApplication ?? ( _exitApplication = new RelayCommand(obj =>
        {
            this.Exit();
        }) );
        #endregion
    }
}
