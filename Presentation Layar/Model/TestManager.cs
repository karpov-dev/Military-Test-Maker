﻿using Data_Layer;
using Presentation_Layar.ViewModel.BaseNavigation;
using Presentation_Layar.ViewModel.Pages;
using Service_Layar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Presentation_Layar.Model
{
    class TestManager : ViewModelBase
    {
        #region Variables
        private Test _test;
        private DataService _dataService;
        private SessionStatistic _statistic;
        private List<Question> _questions;
        private List<ViewModelBase> _tests;
        private int _currentTestIndex = 0;
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
            _tests = new List<ViewModelBase>();
            _currentTestIndex = 0;
            _dataService = DataService.GetInstance();
            _statistic = new SessionStatistic();
            _statistic.AmountQuestions = Settings.PersonalAmountQuestions;

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
            if (_currentTestIndex >= _tests.Count) Root.CurrentVM = Owner;

            _statistic.CurrentQuestion = _currentTestIndex + 1;
            Root.CurrentVM = _tests[_currentTestIndex];
            _currentTestIndex++;
        }

        private void GroupMode()
        {
            _questions = _dataService.GetRandomQuestionsByTest(_test, Settings.GroupAmountQuestions);
            foreach (Question question in _questions )
            {
                _tests.Add(new GroupTestingPageVM(Root, Owner, question, _statistic, this));
            }
            NextTest();
        }
        private void PersonalMode()
        {

        }
        #endregion


    }
}