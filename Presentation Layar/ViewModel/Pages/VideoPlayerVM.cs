using Data_Layer;
using Presentation_Layar.Model;
using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.BaseNavigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Presentation_Layar.ViewModel.Pages
{
    class VideoPlayerVM : ViewModelBase
    {
        private LessonVideo _videoInfo;

        public VideoPlayerVM(RootVM root, object owner, LessonVideo lessonVideo) : base(root, owner)
        {
            _videoInfo = lessonVideo;
        }

        public string VideoPath
        {
            get
            {
                if ( !FileWorker.FileExists(_videoInfo.Path) )
                {
                    MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Root.CurrentVM = Owner;
                }
                return _videoInfo.Path;
            }
        }


        private RelayCommand _cancel;
        public RelayCommand Cancel => _cancel ?? ( _cancel = new RelayCommand(obj =>
        {
            Root.CurrentVM = Owner;
        }));
    }
}
