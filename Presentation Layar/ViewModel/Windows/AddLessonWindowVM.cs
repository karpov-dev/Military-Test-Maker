using Data_Layer;
using Presentation_Layar.Service;
using Presentation_Layar.View.Windows;
using Presentation_Layar.ViewModel.Components;
using Service_Layar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation_Layar.ViewModel.Windows
{
    class AddLessonWindowVM : ModelPropertyChanged
    {
        private DataService _dataService = DataService.GetInstance();

        public AddLessonWindowVM(AddLessonWindow window)
        {
            Window = window;
            Error = new ErrorMessageVM();
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                Error.Hide();
                _title = value;
                OnPropertyChanged();
            }
        }
        public AddLessonWindow Window { get; private set; }
        public ErrorMessageVM Error { get; set; }

        private RelayCommand _cancel;
        public RelayCommand Cancel => _cancel ?? ( _cancel = new RelayCommand(obj =>
        {
            Window.Close();
        }));

        private RelayCommand _save;
        public RelayCommand Save => _save ?? ( _save = new RelayCommand(obj =>
        {
            if ( string.IsNullOrWhiteSpace(Title) )
            {
                Error.Show("Заполните название предмета");
                return;
            }

            Lesson newLesson = new Lesson()
            {
                Title = this.Title
            };
            _dataService.InsertLesson(newLesson);
            Window.Close();
        }));
    }
}
