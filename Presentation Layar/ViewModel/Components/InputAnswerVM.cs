using Microsoft.Win32;
using Presentation_Layar.Model;
using Presentation_Layar.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Presentation_Layar.ViewModel.Components
{
    class InputAnswerVM : ModelPropertyChanged
    {
        #region Constructors
        public InputAnswerVM(int number)
        {
            ErrorCMP = new ErrorMessageVM();
            _useValidation = true;
            Number = number;
        }
        #endregion

        #region Properties
        public ErrorMessageVM ErrorCMP { get; private set; }
        public int Number { get; set; }

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                ErrorCMP.Hide();
                _text = value;
                OnPropertyChanged();
            }
        }

        private bool _useValidation;
        public bool UseValidation
        {
            get => _useValidation;
            set
            {
                _useValidation = value;
                OnPropertyChanged();
            }
        }

        private bool _isChecked;
        public bool IsCheceked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Relay Commands
        private RelayCommand _cleanCommand;
        public RelayCommand CleanCommand
        {
            get => _cleanCommand ??
                ( _cleanCommand = new RelayCommand(obj =>
                {
                    Text = "";
                }) );
        }

        private RelayCommand _addImageCommand;
        public RelayCommand AddImageCommand
        {
            get => _addImageCommand ??
                ( _addImageCommand = new RelayCommand(obj =>
                {
                    Text = FileWorker.OpenFileAndGetPath();
                    if ( string.IsNullOrWhiteSpace(Text) ) ErrorCMP.Show("Возникла ошибка при добавлении файла");
                    if ( FileWorker.FileExists(Text) ) FileWorker.RemoveToRoot(Text);
                }) );
        }
        #endregion

        #region Methods
        public bool CanSave()
        {
            if ( !UseValidation ) return true;
            if ( string.IsNullOrWhiteSpace(Text) )
            {
                ErrorCMP.Show("Заполните поле ответа");
                return false;
            }
            if ( FileWorker.IsImage(Text) && !FileWorker.FileExists(Text) )
            {
                ErrorCMP.Show("Формат файла не подходит или файл не найден");
                return false;
            }
            ErrorCMP.Hide();
            return true;

        }
        #endregion
    }
}
