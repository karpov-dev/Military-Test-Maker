using Data_Layer;
using Presentation_Layar.ViewModel.BaseNavigation;
using Presentation_Layar.ViewModel.Pages;
using Service_Layar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace Presentation_Layar.Model
{
    class TestManager : ViewModelBase
    {
        #region Variables
        private int _currentTestIndex = 0;
        private string _mode;

        private Test _test;
        private Settings _settings;
        private DataService _dataService;
        private DispatcherTimer _timerToTestEnd;
        private TestSessionInformation _statistic;
        private List<Question> _questions;
        private List<ViewModelBase> _tests;
        #endregion


        #region Constructors
        public TestManager(RootVM root, object owner, string mode, Test test) : base(root, owner)
        {
            if ( test == null )
            {
                MessageBox.Show("Возникла ошибка при генерации тестов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Root.CurrentVM = Owner;
            }

            _test = test;
            _currentTestIndex = 0;
            _mode = mode;

            _dataService = DataService.GetInstance();
            _settings = Settings.GetInstance();
            _tests = new List<ViewModelBase>();
            _statistic = new TestSessionInformation();
            _statistic.SetTestTime((int)_settings.TimeToTestEnd);

            _timerToTestEnd = new DispatcherTimer();
            _timerToTestEnd.Tick += TimerToTestEnd_Tick;
            _timerToTestEnd.Interval = new TimeSpan(0, 0, 0, 1);

            switch ( mode )
            {
                case Settings.GROUP_MODE: GroupMode(); break;
                case Settings.PERSONAL_MODE: PersonalMode(); break;
            }
        }
        #endregion


        #region Methods
        public void NextTest()
        {
            if (_currentTestIndex >= _tests.Count )
            {
                _statistic.TestResult = Settings.TEST_SECCSESSFUL;
                GoToResultPage();
                return;
            }
            if(_settings.MaxTestWrongs != 0 && _statistic.WrongQuestions >= _settings.MaxTestWrongs )
            {
                _statistic.TestResult = Settings.TEST_UNSECCSESSFUL;
                GoToResultPage();
            }
            else
            {
                _statistic.CurrentQuestion = _currentTestIndex + 1;
                Root.CurrentVM = _tests[_currentTestIndex];
                _currentTestIndex++;
            }
        }
        public void StopTimer()
        {
            _timerToTestEnd.Stop();
        }
        public void StartTimer()
        {
            _timerToTestEnd.Start();
        }

        private void GroupMode()
        {
            _statistic.AmountQuestions = _settings.GroupAmountQuestions;
            _questions = _dataService.GetRandomQuestionsByTest(_test, _settings.GroupAmountQuestions);
            for(int i = 0; i < _questions.Count; i++ )
            {
                _tests.Add(new GroupTestingPageVM(Root, Owner, _questions[i], _statistic, this));
            }
            NextTest();
        }
        private void PersonalMode()
        {
            _timerToTestEnd.Start();
            _statistic.AmountQuestions = _settings.PersonalAmountQuestions;
            _questions = _dataService.GetRandomQuestionsByTest(_test, _settings.PersonalAmountQuestions);
            for(int i = 0; i < _questions.Count; i++ )
            {
                _tests.Add(new PersonalTestingVM(Root, Owner, _questions[i], _statistic, this));
            }
            NextTest();
        }
        private void TimerToTestEnd_Tick(object sender, EventArgs e)
        {
            _statistic.RemoveSecond();
            if ( _statistic.CurrentMinute < 0)
            {
                _statistic.TestResult = Settings.TEST_TIME_OUT;
                GoToResultPage();
            }  
        }
        private void GoToResultPage()
        {
            _timerToTestEnd.Stop();
            switch ( _mode )
            {
                case Settings.GROUP_MODE: Root.CurrentVM = new GroupTestingPageResultVM(Root, Owner); break;
                case Settings.PERSONAL_MODE: Root.CurrentVM = new PersonalTestingPageResult(Root, Owner, _statistic); break;
            }
        }
        #endregion
    }
}
