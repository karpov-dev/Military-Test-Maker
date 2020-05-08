using Presentation_Layar.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation_Layar.ViewModel.Components
{
    class InputTextVM : ModelPropertyChanged
    {
        #region Constructors
        public InputTextVM()
        {
            ErrorCMP = new ErrorMessageVM();
            useValidation = true;
        }
        #endregion

        #region Properties
        public ErrorMessageVM ErrorCMP { get; private set; }

        private string text;
        public string Text
        {
            get => text;
            set
            {
                ErrorCMP.Hide();
                text = value;
                OnPropertyChanged();
            }
        }

        private bool useValidation;
        public bool UseValidation
        {
            get => useValidation;
            set
            {
                useValidation = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Relay Commands
        private RelayCommand clean;
        public RelayCommand Clean
        {
            get => clean ??
                ( clean = new RelayCommand(obj =>
                {
                    Text = "";
                }) );
        }
        #endregion

        #region Methods
        public bool CanSave()
        {
            if ( !UseValidation ) return true;
            if ( !string.IsNullOrWhiteSpace(Text) ) return true;
            ErrorCMP.Show("Заполните пустое поле");
            return false;
        }
        #endregion
    }
}
