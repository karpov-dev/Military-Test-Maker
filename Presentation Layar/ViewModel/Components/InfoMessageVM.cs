using Presentation_Layar.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Presentation_Layar.ViewModel.Components
{
    class InfoMessageVM : ModelPropertyChanged 
    {
        #region Constructors
        public InfoMessageVM()
        {
            visibility = Visibility.Collapsed;
        }
        #endregion

        #region Message
        private string text;
        public string Text
        {
            get => text;
            private set
            {
                text = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Visibility
        private Visibility visibility;
        public Visibility Visibility
        {
            get => visibility;
            private set
            {
                visibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods
        public void Show(string text)
        {
            Text = text;
            Visibility = Visibility.Visible;
        }
        public void Hide()
        {
            Visibility = Visibility.Collapsed;
            Text = "";
        }
        #endregion
    }
}
