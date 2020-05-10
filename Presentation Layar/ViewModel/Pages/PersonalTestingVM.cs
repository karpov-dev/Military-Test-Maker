using Data_Layer;
using Presentation_Layar.Model;
using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.BaseNavigation;
using Presentation_Layar.ViewModel.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace Presentation_Layar.ViewModel.Pages
{
    class PersonalTestingVM : ViewModelBase
    {
        #region Variables 
        private TestManager _manager;
        private DispatcherTimer _timerToAnswer;
        private DispatcherTimer _timerToNextQuestion;
        private Question _question;
        private double _timeToNextQuestion = 2;
        #endregion

        #region Properties
        public ErrorMessageVM Error { get; set; }
        public InfoMessageVM Info { get; set; }

        private TestStatistic _statistic;
        public TestStatistic Statistic
        {
            get => _statistic;
            set
            {
                _statistic = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AnswerViewVM> AnswerViews { get; set; }

        public string Question => _question.Queston;
        public List<string> Answers => _question.Answers;

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

        public bool ButtonsEnabled { get; set; }

        #region Visibility
        private Visibility _oneVisibility;
        public Visibility OneVisibility
        {
            get => _oneVisibility;
            set
            {
                _oneVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _twoVisibility;
        public Visibility TwoVisibility
        {
            get => _twoVisibility;
            set
            {
                _twoVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _threeVisibility;
        public Visibility ThreeVisibility
        {
            get => _threeVisibility;
            set
            {
                _threeVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _fourVisibility;
        public Visibility FourVisibility
        {
            get => _fourVisibility;
            set
            {
                _fourVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #endregion

        #region Constructors
        public PersonalTestingVM(RootVM root, object owner, Question question, TestStatistic statistic, TestManager manager) : base(root, owner)
        {
            if ( question == null || statistic == null || manager == null )
            {
                MessageBox.Show("Возникла ошибка при загрузке теста", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Root.CurrentVM = Owner;
            }

            _question = question;
            Statistic = statistic;
            _manager = manager;

            InitializationObjects();
            InitializationAnswers();
            InitializationTimerToAnswer();
        }
        #endregion

        #region Relay Commands 
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? ( _cancelCommand = new RelayCommand(obj =>
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите закончить тестирование", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if ( result == MessageBoxResult.Yes ) Root.CurrentVM = Owner;
        }) );

        private RelayCommand _checkAnswer;
        public RelayCommand CheckAnswer => _checkAnswer ?? ( _checkAnswer = new RelayCommand(obj =>
        {
            AnswerViewVM selectedAnswer = (AnswerViewVM) obj;
            if ( selectedAnswer.Text != _question.RightAnswer ) Error.Show("Вы выбрали неправильный ответ");
            else
            {
                Info.Show("Правильно");
                _statistic.RightQuestions++;
            }
            _timerToAnswer.Stop();
            InitializationTimerToNextQuestion();
            HideWrongAnswers();
        }));
        #endregion

        #region Methods 
        private void TimerToAnswer_Tick(object sender, EventArgs e)
        {
            TimerTime -= 0.1;
            if ( TimerTime < 0 )
            {
                _timerToAnswer.Stop();
                TimerTime = 0;
                Error.Show("Время на ответ истекло!");
                HideWrongAnswers();
            }
        }
        private void TimerToNextQuestion_Tick(object sender, EventArgs e)
        {
            TimerTime -= 0.1;
            if(TimerTime < 0 )
            {
                _timerToNextQuestion.Stop();
                _manager.NextTest();
            }
        }
        private void HideWrongAnswers()
        {
            //костыль всея руси
            if ( _question.Answers[0] != _question.RightAnswer ) OneVisibility = Visibility.Collapsed;
            if ( _question.Answers[1] != _question.RightAnswer ) TwoVisibility = Visibility.Collapsed;
            if ( _question.Answers[2] != _question.RightAnswer ) ThreeVisibility = Visibility.Collapsed;
            if ( _question.Answers[3] != _question.RightAnswer ) FourVisibility = Visibility.Collapsed;
            ButtonsEnabled = false;
            OnPropertyChanged("ButtonsEnabled");
        }

        private void InitializationAnswers()
        {
            for ( int i = 0; i < _question.Answers.Count; i++ )
            {
                AnswerViews.Add(new AnswerViewVM(i + 1, _question.Answers[i]));
            }
        }
        private void InitializationTimerToAnswer()
        {
            _timerToAnswer = new DispatcherTimer();
            _timerToAnswer.Tick += TimerToAnswer_Tick;
            _timerToAnswer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timerToAnswer.Start();
        }
        private void InitializationTimerToNextQuestion()
        {
            _timerToNextQuestion = new DispatcherTimer();
            _timerToNextQuestion.Tick += TimerToNextQuestion_Tick;
            _timerToNextQuestion.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timerToNextQuestion.Start();
        }
        private void InitializationObjects()
        {
            Error = new ErrorMessageVM();
            Info = new InfoMessageVM();
            TimerTime = Settings.PersonalTimeToAnswer;
            AnswerViews = new ObservableCollection<AnswerViewVM>();
            ButtonsEnabled = true;
        }
        #endregion
    }
}
