using Data_Layer;
using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation_Layar.ViewModel.Windows
{
    class PDFConverterSettingsVM : ModelPropertyChanged 
    {
        #region Properties
        public int TitleFontSize
        {
            get => PDFCreator.TitleFontSize;
            set
            {
                PDFCreator.TitleFontSize = value;
            }
        }
        public int BigFontSize
        {
            get => PDFCreator.BigFontSize;
            set
            {
                PDFCreator.BigFontSize = value;
            }
        }
        public int MainFontSize
        {
            get => PDFCreator.MainFontSize;
            set
            {
                PDFCreator.MainFontSize = value;
            }
        }
        public int SmallFontSize
        {
            get => PDFCreator.SmallFontSize;
            set
            {
                PDFCreator.SmallFontSize = value;
            }
        }
        public int QuestionOffset
        {
            get => PDFCreator.QuestionOffset;
            set
            {
                PDFCreator.QuestionOffset = value;
            }
        }
        public float LineSpacing
        {
            get => PDFCreator.LineSpacing;
            set
            {
                PDFCreator.LineSpacing = value;
            }
        }
        public float WordSpacing
        {
            get => PDFCreator.WordSpacing;
            set
            {
                PDFCreator.WordSpacing = value;
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
            HideNotifications();
            Info.Show("Формирование файла");
            bool convertResult = PDFCreator.CreatePDF(Test);
            Info.Hide();
            if ( convertResult ) Info.Show("PDF файл успешно сформирован!");
            else Error.Show("Возникла ошибка при формировании PDF файла");
        }));
        #endregion

        #region Methods
        private void HideNotifications()
        {
            Info.Hide();
            Error.Hide();
        }
        #endregion
    }
}
