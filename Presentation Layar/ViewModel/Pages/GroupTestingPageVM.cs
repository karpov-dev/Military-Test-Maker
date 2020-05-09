using Data_Layer;
using Presentation_Layar.Model;
using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.BaseNavigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace Presentation_Layar.ViewModel.Pages
{
    class GroupTestingPageVM : ViewModelBase
    {
        #region Variables 
        private TestManager _manager;
        private DispatcherTimer _timer;
        #endregion

        #region Properties
        public Question Question { get; set; }
        public SessionStatistic Statistic { get; set; }

        private double _timerTime;
        public double TimerTime
        {
            get => _timerTime;
            set
            {
                _timerTime = Math.Round(value, 2);
                OnPropertyChanged();
            }
        }

        public Visibility OneVisibility { get; set; }
        public Visibility TwoVisibility { get; set; }
        public Visibility ThreeVisibility { get; set; }
        public Visibility FourVisibility { get; set; }
        #endregion

        #region Constructors
        public GroupTestingPageVM(RootVM root, object owner, Question question, SessionStatistic statistic, TestManager manager) : base(root, owner)
        {
            if ( question == null || statistic == null || manager == null )
            {
                MessageBox.Show("Возникла ошибка при загрузке теста", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Root.CurrentVM = Owner;
            }
            Question = question;
            Statistic = statistic;
            _manager = manager;
            TimerTime = Settings.GroupTimeToNextQuestion;
        }
        #endregion

        #region Relay Commands 
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? ( _cancelCommand = new RelayCommand(obj =>
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите закончить тестирование", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if ( result == MessageBoxResult.Yes ) Root.CurrentVM = Owner;
        }));

        private RelayCommand _showRightAnswer;
        public RelayCommand ShowRightAnswer => _showRightAnswer ?? ( _showRightAnswer = new RelayCommand(obj =>
        {
            HideWrongAnswers();
            _timer = new DispatcherTimer();
            _timer.Tick += TimerTick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Start();
        }));
        #endregion

        #region Methods 
        private void TimerTick(object sender, EventArgs e)
        {
            TimerTime -= 0.1;
            if ( TimerTime < 0 )
            {
                _timer.Stop();
                TimerTime = 0;
                _manager.NextTest();
            }
        }
        private void HideWrongAnswers()
        {
            //костыль всея руси
            if ( Question.Answers[0] != Question.RightAnswer ) OneVisibility = Visibility.Hidden;
            if ( Question.Answers[1] != Question.RightAnswer ) TwoVisibility = Visibility.Hidden;
            if ( Question.Answers[2] != Question.RightAnswer ) ThreeVisibility = Visibility.Hidden;
            if ( Question.Answers[3] != Question.RightAnswer ) FourVisibility = Visibility.Hidden;

            OnPropertyChanged("OneVisibility");
            OnPropertyChanged("TwoVisibility");
            OnPropertyChanged("ThreeVisibility");
            OnPropertyChanged("FourVisibility");
        }
        #endregion
    }
}
