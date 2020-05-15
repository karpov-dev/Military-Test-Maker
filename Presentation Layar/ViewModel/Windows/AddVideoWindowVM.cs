using Data_Layer;
using Presentation_Layar.Model;
using Presentation_Layar.Service;
using Presentation_Layar.View.Windows;
using Presentation_Layar.ViewModel.Components;
using Service_Layar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Layar.ViewModel.Windows
{
    class AddVideoWindowVM : ModelPropertyChanged
    {
        private DataService _dataService = DataService.GetInstance();
        private AddVideoWindow _window;
        private Lesson _lesson;

        public AddVideoWindowVM(AddVideoWindow window, Lesson lesson)
        {
            _window = window;
            _lesson = lesson;
            Error = new ErrorMessageVM();
            Info = new InfoMessageVM();
            ButtonsEnabled = true;
        }

        private string _videoTitle;
        public string VideoTitle
        {
            get => _videoTitle;
            set
            {
                _videoTitle = value;
                OnPropertyChanged();
            }
        }

        private string _videoPath;
        public string VideoPath
        {
            get => _videoPath;
            set
            {
                _videoPath = value;
                OnPropertyChanged();
            }
        }

        private bool _buttonsEnabled;
        public bool ButtonsEnabled
        {
            get => _buttonsEnabled;
            set
            {
                _buttonsEnabled = value;
                OnPropertyChanged();
            }
        }

        public ErrorMessageVM Error { get; set; }
        public InfoMessageVM Info { get; set; }


        private RelayCommand _cancel;
        public RelayCommand Cancel => _cancel ?? ( _cancel = new RelayCommand(obj =>
        {
            _window.Close();
        }));

        private RelayCommand _save;
        public RelayCommand Save => _save ?? ( _save = new RelayCommand(obj =>
        {
            if ( string.IsNullOrWhiteSpace(VideoTitle) )
            {
                Error.Show("Заполните название видео");
                return;
            }
            if ( string.IsNullOrWhiteSpace(VideoPath) )
            {
                Error.Show("Добавьте видеоматериал");
                return;
            }
            if ( !FileWorker.FileExists(VideoPath) )
            {
                Error.Show("Файл не найден");
                return;
            }
            _dataService.InsertLessonVideo(_lesson, VideoTitle, VideoPath);
            _window.Close();
        }));

        private RelayCommand _addPath;
        public RelayCommand AddPath => _addPath ?? ( _addPath = new RelayCommand(obj =>
        {
            VideoPath = FileWorker.OpenFileAndGetPath("Video Files(*.mp4;*.avi;*.mov;*.wmv)|*.mp4;*.avi;*.mov;*.wmv|All files (*.*)|*.*");
            if ( string.IsNullOrEmpty(VideoPath) ) Error.Show("Путь не добавлен");
            if ( FileWorker.FileExists(VideoPath) ) RemoveFile();
        }));


        private async void RemoveFile()
        {
            ButtonsEnabled = false;
            Info.Show("Копирование видео...");
            await Task.Run( () => FileWorker.RemoveToRoot(VideoPath));
            Info.Show("Видео скопировано");
            ButtonsEnabled = true;
        }
    }
}
