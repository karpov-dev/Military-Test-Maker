using Data_Layer;
using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Layar.ViewModel.Windows
{
    class PDFConverterSettingsVM : ModelPropertyChanged 
    {
        Settings _settings = Settings.GetInstance();

        #region Properties
        public int TitleFontSize
        {
            get => _settings.TitleFontSize;
            set
            {
                _settings.TitleFontSize = value;
            }
        }
        public int BigFontSize
        {
            get => _settings.BigFontSize;
            set
            {
                _settings.BigFontSize = value;
            }
        }
        public int MainFontSize
        {
            get => _settings.MainFontSize;
            set
            {
                _settings.MainFontSize = value;
            }
        }
        public int SmallFontSize
        {
            get => _settings.SmallFontSize;
            set
            {
                _settings.SmallFontSize = value;
            }
        }
        public int QuestionOffset
        {
            get => _settings.QuestionOffset;
            set
            {
                _settings.QuestionOffset = value;
            }
        }
        public float LineSpacing
        {
            get => _settings.LineSpacing;
            set
            {
                _settings.LineSpacing = value;
            }
        }
        public float WordSpacing
        {
            get => _settings.WordSpacing;
            set
            {
                _settings.WordSpacing = value;
            }
        }
        public string FilePath
        {
            get => PDFCreator.FilePath;
            set
            {
                PDFCreator.FilePath = value;
            }
        }
        public string FileName
        {
            get => PDFCreator.FileName;
            set
            {
                PDFCreator.FileName = value;
            }
        }

        public Test Test { get; set; }
        public ErrorMessageVM Error { get; set; }
        public InfoMessageVM Info { get; set; }
        #endregion

        #region Constructors
        public PDFConverterSettingsVM(Test test)
        {
            Test = test;
            Error = new ErrorMessageVM();
            Info = new InfoMessageVM();
        }
        #endregion

        #region Relay Commands
        private RelayCommand _convertIt;
        public RelayCommand ConvertToPDF => _convertIt ?? ( _convertIt = new RelayCommand(obj =>
        {
            ConvertIt();
        }));
        #endregion

        #region Methods
        private void HideNotifications()
        {
            Info.Hide();
            Error.Hide();
        }
        private async void ConvertIt()
        {
            HideNotifications();
            Info.Show("Начато формирование PDF файла");
            bool convertResult = await Task.Run(() => PDFCreator.CreatePDF(Test));
            if ( convertResult ) Info.Show("PDF файл успешно сформирован!");
            else Error.Show("Возникла ошибка при формировании PDF файла");
        }
        #endregion
    }
}
