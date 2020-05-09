using Data_Layer;
using Presentation_Layar.Service;
using Presentation_Layar.View.Windows;
using Presentation_Layar.ViewModel.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation_Layar.ViewModel.Windows
{
    class TestingSettingsVM : ModelPropertyChanged
    {
        #region Constructors
        public TestingSettingsVM(TestingSettings window)
        {
            Window = window;
            Error = new ErrorMessageVM();
        }
        #endregion

        #region Properties
        public ErrorMessageVM Error { get; set; }
        public TestingSettings Window { get; set; }

        #region Group Settings
        private int _groupAmountQuestions = Settings.GroupAmountQuestions;
        public int GroupAmountQuestions
        {
            get => _groupAmountQuestions;
            set
            {
                Error.Hide();
                if(value <= Settings.MAX_GROUP_AMOUNT_QUESTIONS && value >= Settings.MIN_GROUP_AMOUNT_QUESTIONS ) _groupAmountQuestions = value;
                else Error.Show("Количество вопросов для группы должно быть в диапозоне от " + Settings.MIN_GROUP_AMOUNT_QUESTIONS + " до " + Settings.MAX_GROUP_AMOUNT_QUESTIONS);
            }
        }

        private double _groupTimeToNextQuestion = Settings.GroupTimeToNextQuestion;
        public double GroupTimeToNextQuestion
        {
            get => _groupTimeToNextQuestion;
            set
            {
                Error.Hide();
                if ( value <= Settings.MAX_GROUP_TIME_TO_NEXT_QUESTION && value >= Settings.MIN_GROUP_TIME_TO_NEXT_QUESTION ) _groupTimeToNextQuestion = value;
                else Error.Show("Время перехода к следующему вопросу должно быть в диапазоне от " + Settings.MIN_GROUP_TIME_TO_NEXT_QUESTION + " до " + Settings.MAX_GROUP_TIME_TO_NEXT_QUESTION);
            }
        }

        public bool ShowRightAnswer { get; set; } = Settings.ShowRightAnswer;
        #endregion

        #region Personal Settings
        private double _personalTimeToAnswer = Settings.PersonalTimeToAnswer;
        public double PersonalTimeToAnswer
        {
            get => _personalTimeToAnswer;
            set
            {
                Error.Hide();
                if ( value <= Settings.MAX_PERSONAL_TIME_TO_NEXT_QURSTION && value >= Settings.MIN_GROUP_TIME_TO_NEXT_QUESTION ) _personalTimeToAnswer = value;
                else Error.Show("Время на ответ должно быть в диапозоне от " + Settings.MIN_GROUP_TIME_TO_NEXT_QUESTION + " до " + Settings.MAX_PERSONAL_TIME_TO_NEXT_QURSTION);
            }
        }

        private int _personalAmountQuestions = Settings.PersonalAmountQuestions;
        public int PersonalAmountQuestions
        {
            get => _personalAmountQuestions;
            set
            {
                Error.Hide();
                if ( value <= Settings.MAX_PERSONAL_AMOUNT_QUESTIONS && value >= Settings.MIN_PERSONAL_AMOUNT_QUESTIONS ) _personalAmountQuestions = value;
                else Error.Show("Количество вопросов должно быть в диапазоне от " + Settings.MIN_PERSONAL_AMOUNT_QUESTIONS + " до " + Settings.MAX_PERSONAL_AMOUNT_QUESTIONS);
            }
        }
        #endregion

        #endregion

        #region RelayCommands
        private RelayCommand _saveSettings;
        public RelayCommand SaveSettings => _saveSettings ?? ( _saveSettings = new RelayCommand(obj =>
        {
            Settings.GroupAmountQuestions = GroupAmountQuestions;
            Settings.GroupTimeToNextQuestion = GroupTimeToNextQuestion;
            Settings.ShowRightAnswer = ShowRightAnswer;
            Settings.PersonalTimeToAnswer = PersonalTimeToAnswer;
            Settings.PersonalAmountQuestions = PersonalAmountQuestions;
            Window.Hide();
        }));

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? ( _cancelCommand = new RelayCommand(obj =>
        {
            Window.Hide();
        }));
        #endregion
    }
}
