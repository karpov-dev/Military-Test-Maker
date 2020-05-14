using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.BaseNavigation;
using Presentation_Layar.ViewModel.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation_Layar.ViewModel.Pages
{
    class PasswordPageVM : ViewModelBase
    {
        public PasswordPageVM(RootVM root, object owner, ViewModelBase nextPage, string password) : base(root, owner)
        {
            _password = password;
            _nextPage = nextPage;
            Error = new ErrorMessageVM();
            PasswordIsTrue = false;
        }

        #region Variables
        private string _password;
        private ViewModelBase _nextPage;
        #endregion

        #region Properties
        private string _passwordInput;
        public string Password
        {
            get => _passwordInput;
            set
            {
                Error.Hide();
                _passwordInput = value;
                OnPropertyChanged();
                OnPropertyChanged("NextButtonEnabled");
            }
        }

        public bool NextButtonEnabled
        {
            get
            {
                if ( string.IsNullOrWhiteSpace(Password) ) return false;
                return true;
            }
        }
        public bool PasswordIsTrue { get; set; }

        public ErrorMessageVM Error { get; set; }
        #endregion

        #region Relay Command
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? ( _cancelCommand = new RelayCommand(obj =>
        {
            Root.CurrentVM = Owner;
        }));

        private RelayCommand _nextCommand;
        public RelayCommand NextCommand => _nextCommand ?? ( _nextCommand = new RelayCommand(obj =>
        {
            if ( string.IsNullOrWhiteSpace(Password) ) Error.Show("Заполните поле ввода пароля");
            if ( Password != _password ) Error.Show("Неверный пароль.");
            if ( Password == _password )
            {
                PasswordIsTrue = true;
                Root.CurrentVM = _nextPage;
            }
        }));
        #endregion
    }
}
