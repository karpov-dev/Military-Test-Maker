using Data_Layer;
using Presentation_Layar.Model;
using Presentation_Layar.Service;
using Presentation_Layar.ViewModel.BaseNavigation;
using Presentation_Layar.ViewModel.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Presentation_Layar.ViewModel.Pages
{
    class PersonalTestingVM : ViewModelBase
    {
        #region Variables 
        private TestManager _manager;
        private DispatcherTimer _timerToNextQuestion;
        private Question _question;
        private Settings _settings;
        private double _timeToNextQuestion = 2;
        #endregion

        #region Properties
        public ErrorMessageVM Error { get; set; }
        public InfoMessageVM Info { get; set; }

        private TestSessionInformation _statistic;
        public TestSessionInformation Statistic
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
        public PersonalTestingVM(RootVM root, object owner, Question question, TestSessionInformation statistic, TestManager manager) : base(root, owner)
        {
            if ( question == null || statistic == null || manager == null )
            {
                MessageBox.Show("Возникла ошибка при загрузке теста", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Root.CurrentVM = Owner;
            }

            _settings = Settings.GetInstance();
            _question = question;
            Statistic = statistic;
            _manager = manager;

            InitializationObjects();
            InitializationAnswers();
        }
        #endregion

        #region Relay Commands 
        public RelayCommand CancelCommand => new RelayCommand(obj =>
        {
            _timerToNextQuestion.Stop();
            MessageBoxResult result = MessageBox.Show("Вы точно хотите закончить тестирование", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if ( result == MessageBoxResult.Yes ) Root.CurrentVM = Owner;
            else _timerToNextQuestion.Start();
        });

        private RelayCommand _checkAnswer;
        public RelayCommand CheckAnswer => _checkAnswer ?? ( _checkAnswer = new RelayCommand(obj =>
        {
            AnswerViewVM selectedAnswer = (AnswerViewVM) obj;
            if ( selectedAnswer.Text != _question.RightAnswer ) 
            {
                Error.Show("Вы выбрали неправильный ответ");
                _statistic.WrongQuestions++;
            } 
            else
            {
                Info.Show("Правильно");
                _statistic.RightQuestions++;
            }
            HideWrongAnswers();
        }));
        #endregion

        #region Methods 
        private void TimerToAnswer_Tick(object sender, EventArgs e)
        {
            TimerTime -= 0.1;
            if ( TimerTime < 0 )
            {
                TimerTime = 0;
                Error.Show("Время на ответ истекло!");
                HideWrongAnswers();
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
            _timerToNextQuestion.Start();
        }

        private void TimerToNextQuestion_Tick(object sender, EventArgs e)
        {
            _timeToNextQuestion -= 0.1;
            if(_timeToNextQuestion < 0 )
            {
                _timerToNextQuestion.Stop();
                _manager.NextTest();
            }
        }

        private void InitializationAnswers()
        {
            for ( int i = 0; i < _question.Answers.Count; i++ )
            {
                AnswerViews.Add(new AnswerViewVM(i + 1, _question.Answers[i]));
            }
        }
        private void InitializationObjects()
        {
            Error = new ErrorMessageVM();
            Info = new InfoMessageVM();
            TimerTime = _settings.PersonalTimeToAnswer;
            AnswerViews = new ObservableCollection<AnswerViewVM>();
            ButtonsEnabled = true;
            _timerToNextQuestion = new DispatcherTimer();
            _timerToNextQuestion.Tick += TimerToNextQuestion_Tick;
            _timerToNextQuestion.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }
        #endregion
    }
}
