using Data_Layer;
using Presentation_Layar.Service;
using Presentation_Layar.View.Windows;
using Presentation_Layar.ViewModel.BaseNavigation;
using Presentation_Layar.ViewModel.Components;
using Presentation_Layar.ViewModel.Windows;
using Service_Layar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Presentation_Layar.ViewModel.Pages
{
    class LearningPageVM : ViewModelBase
    {
        #region Variables
        private DataService dataService = DataService.GetInstance();
        #endregion

        public LearningPageVM(RootVM root, object owner) : base(root, owner)
        {
            Error = new ErrorMessageVM();
            Info = new InfoMessageVM();
            SetDefaultSelectedLesson();
        }

        #region Properties
        public ObservableCollection<Lesson> Lessons => new ObservableCollection<Lesson>(dataService.GetLessons());
        public ErrorMessageVM Error { get; set; }
        public InfoMessageVM Info { get; set; }

        private Lesson _selectedLesson;
        public Lesson SelectedLesson
        {
            get => _selectedLesson;
            set
            {
                _selectedLesson = value;
                OnPropertyChanged();
                OnPropertyChanged("LessonsButtonEnabled");
            }
        }

        private LessonVideo _selectedVideo;
        public LessonVideo SelectedVideo
        {
            get => _selectedVideo;
            set
            {
                _selectedVideo = value;
                OnPropertyChanged();
                OnPropertyChanged("VideoButtonsEnabled");
            }
        }

        public bool LessonsButtonEnabled
        {
            get
            {
                if ( SelectedLesson != null ) return true;
                return false;
            }
        }
        public bool VideoButtonsEnabled
        {
            get
            {
                if ( SelectedVideo != null ) return true;
                return false;
            }
        }
        #endregion

        #region Relay Commands
        private RelayCommand _start;
        public RelayCommand Start => _start ?? ( _start = new RelayCommand(obj =>
        {
            Root.CurrentVM = new VideoPlayerVM(Root, this, SelectedVideo);
        }));

        private RelayCommand _addLesson;
        public RelayCommand AddLesson => _addLesson ?? ( _addLesson = new RelayCommand(obj =>
        {
            AddLessonWindow addLessonWindow = new AddLessonWindow();
            addLessonWindow.DataContext = new AddLessonWindowVM(addLessonWindow);
            addLessonWindow.Closed += AddLessonWindow_Closed;
            addLessonWindow.Show();
        }));

        private RelayCommand _removeLesson;
        public RelayCommand RemoveLesson => _removeLesson ?? ( _removeLesson = new RelayCommand(obj =>
        {
            if ( SelectedLesson == null ) return;

            dataService.RemoveLesson(SelectedLesson);
            SetDefaultSelectedLesson();
            UpdateComponent();
        }));

        private RelayCommand _cancel;
        public RelayCommand Cancel => _cancel ?? ( _cancel = new RelayCommand(obj =>
        {
            Root.CurrentVM = Owner;
        }));

        private RelayCommand _addVideo;
        public RelayCommand AddVideo => _addVideo ?? ( _addVideo = new RelayCommand(obj =>
        {
            if ( SelectedLesson == null ) return;

            AddVideoWindow addVideoWindow = new AddVideoWindow();
            addVideoWindow.DataContext = new AddVideoWindowVM(addVideoWindow, SelectedLesson);
            addVideoWindow.Closed += AddLessonWindow_Closed;
            addVideoWindow.Show();
        }));

        private RelayCommand _removeVideo;
        public RelayCommand RemoveVideo => _removeVideo ?? ( _removeVideo = new RelayCommand(obj =>
        {
            if ( SelectedVideo == null ) return;

            dataService.RemoveLessonVideo(SelectedLesson, SelectedVideo);
            UpdateComponent();
        }));
        #endregion

        #region Methods 
        private void AddLessonWindow_Closed(object sender, EventArgs e)
        {
            UpdateComponent();
        }
        private void UpdateComponent()
        {
            SetDefaultSelectedLesson();
            OnPropertyChanged("SelectedLesson");
            OnPropertyChanged("SelectedVideo");
            OnPropertyChanged("Lessons");

            #region НЕ ОТКРЫВАТЬ 
            #region Я предупредил
            #region ОСТАНОВИСЬ ГЛУПЕЦ
            #region Ну ладно, ты упорный...
            #region 3
            #region 2
            #region 1
            #region 0
            #region -1
            //Вашему вниманию - костыль собственной персоны
            Lesson сrutch = SelectedLesson;
            SelectedLesson = null;
            SelectedLesson = сrutch;
            #endregion
            #endregion
            #endregion
            #endregion
            #endregion
            #endregion
            #endregion
            #endregion
            #endregion

            Info.Hide();
            Error.Hide();
        }
        private void SetDefaultSelectedLesson()
        {
            try
            {
                if ( Lessons == null || Lessons.Count == 0 ) return;
                SelectedLesson = Lessons[0];
            }
            catch
            {

            }
            
        }
        #endregion
    }
}
