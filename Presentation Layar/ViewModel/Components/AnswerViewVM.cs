using Data_Layer;
using Presentation_Layar.Model;
using Presentation_Layar.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Presentation_Layar.ViewModel.Components
{
    class AnswerViewVM : ModelPropertyChanged 
    {
        #region Properies
        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private int _number;
        public int Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }

        private int _imageWidth = Settings.ImageWidht; 
        public int ImageWidth
        {
            get => _imageWidth;
            set
            {
                _imageWidth = value;
                OnPropertyChanged();
            }
        }

        private int _imageHeight = Settings.ImageHeight;
        public int ImageHeight
        {
            get => _imageHeight;
            set
            {
                _imageHeight = value;
                OnPropertyChanged();
            }
        }

        private Visibility _textBlockVisibility;
        public Visibility TextBlockVisibility
        {
            get => _textBlockVisibility;
            set
            {
                _textBlockVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _imageVisibility;
        public Visibility ImageVisibility
        {
            get => _imageVisibility;
            set
            {
                _imageVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public AnswerViewVM(int number, string text)
        {
            if ( text == null ) return;
            Text = text;
            Number = number;
            if ( FileWorker.FileExists(text) && FileWorker.IsImage(text) ) ShowImage();
            else ShowText();
        }
        #endregion

        #region Methods
        private void ShowText()
        {
            TextBlockVisibility = Visibility.Visible;
            ImageVisibility = Visibility.Collapsed;
        }
        private void ShowImage()
        {
            TextBlockVisibility = Visibility.Collapsed;
            ImageVisibility = Visibility.Visible;
        }
        #endregion
    }
}
