using Data_Layer;
using Presentation_Layar.Service;
using Presentation_Layar.View.Windows;
using Presentation_Layar.ViewModel.BaseNavigation;
using Presentation_Layar.ViewModel.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation_Layar.ViewModel.Pages
{
    class TestingSettingsVM : ViewModelBase
    {
        #region Variables
        private Settings _settings = Settings.GetInstance();
        #endregion

        #region Constructors
        public TestingSettingsVM(RootVM root, object owner): base(root, owner)
        {
            Error = new ErrorMessageVM();
            GroupAmountQuestions = _settings.GroupAmountQuestions;
            GroupTimeToNextQuestion = _settings.GroupTimeToNextQuestion;
            PersonalAmountQuestions = _settings.PersonalAmountQuestions;
            PersonalTimeToNextQuestion = _settings.PersonalTimeToNextQuestion;
            ImageHeight = _settings.ImageHeight;
            ImageWidth = _settings.ImageWidht;
            MaxWrongs = _settings.MaxTestWrongs;
            PersonalTestTime = _settings.TimeToTestEnd;
            Password = _settings.Password;
        }
        #endregion

        #region Properties
        public ErrorMessageVM Error { get; set; }
        public TestingSettings Window { get; set; }

        #region Group Settings
        private int _groupAmountQuestions;
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

        private double _groupTimeToNextQuestion;
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

        public bool ShowRightAnswer { get; set; }
        #endregion

        #region Personal Settings
        private double _personalTimeToNextQuestion;
        public double PersonalTimeToNextQuestion
        {
            get => _personalTimeToNextQuestion;
            set
            {
                Error.Hide();
                if ( value <= Settings.MAX_PERSONAL_TIME_TO_NEXT_QURSTION && value >= Settings.MIN_GROUP_TIME_TO_NEXT_QUESTION ) _personalTimeToNextQuestion = value;
                else Error.Show("Время на ответ должно быть в диапозоне от " + Settings.MIN_GROUP_TIME_TO_NEXT_QUESTION + " до " + Settings.MAX_PERSONAL_TIME_TO_NEXT_QURSTION);
            }
        }

        private int _personalAmountQuestions;
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

        private double _personalTestTime;
        public double PersonalTestTime
        {
            get => _personalTestTime;
            set
            {
                Error.Hide();
                if (value <= Settings.MAX_PERSONAL_TIME_TO_TEST_END && value >= Settings.MIN_PERSONAL_TIME_TEST_END ) _personalTestTime = value;
                else Error.Show("Время должно быть в диапазоне от " + Settings.MIN_PERSONAL_TIME_TEST_END + " сек. до " + Settings.MAX_PERSONAL_TIME_TO_TEST_END + " сек.");
            }
        }

        private int _maxWrongs;
        public int MaxWrongs
        {
            get => _maxWrongs;
            set
            {
                Error.Hide();
                if ( _maxWrongs <= Settings.MAX_PERSONA_TEST_WRONGS && _maxWrongs >= Settings.MIN_PERSONAL_TEST_WRONGS ) _maxWrongs = value;
                else Error.Show("Количество ошибок должно быть в диапозоне от " + Settings.MIN_PERSONAL_TEST_WRONGS + " до " + Settings.MAX_PERSONA_TEST_WRONGS);
            }
        }
        #endregion

        #region General Settings
        private int _imageWidth;
        public int ImageWidth
        {
            get => _imageWidth;
            set
            {
                Error.Hide();
                if ( value <= Settings.MAX_IMAGE_WIDTH && value >= Settings.MIN_IMAGE_WIDTH ) _imageWidth = value;
                else Error.Show("Ширина изображения должна быть в диапозоне от " + Settings.MIN_IMAGE_WIDTH + " до " + Settings.MAX_IMAGE_WIDTH);
            }
        }

        private int _imageHeight;
        public int ImageHeight
        {
            get => _imageHeight;
            set
            {
                Error.Hide();
                if ( value <= Settings.MAX_IMAGE_HEIGHT && value >= Settings.MIN_IMAGE_HEIGHT ) _imageHeight = value;
                else Error.Show("Высота изображения должна быть в диапозоне от " + Settings.MIN_IMAGE_HEIGHT + " до " + Settings.MAX_IMAGE_HEIGHT);
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
            }
        }
        #endregion

        #endregion

        #region RelayCommands
        private RelayCommand _saveSettings;
        public RelayCommand SaveSettings => _saveSettings ?? ( _saveSettings = new RelayCommand(obj =>
        {
            _settings.GroupAmountQuestions = GroupAmountQuestions;
            _settings.GroupTimeToNextQuestion = GroupTimeToNextQuestion;
            _settings.PersonalTimeToNextQuestion = PersonalTimeToNextQuestion;
            _settings.PersonalAmountQuestions = PersonalAmountQuestions;
            _settings.ImageHeight = ImageHeight;
            _settings.ImageWidht = ImageWidth;
            _settings.TimeToTestEnd = PersonalTestTime;
            _settings.MaxTestWrongs = MaxWrongs;
            _settings.Password = Password;
            Root.CurrentVM = Owner;
        }));

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? ( _cancelCommand = new RelayCommand(obj =>
        {
            Root.CurrentVM = Owner;
        }));
        #endregion
    }
}
