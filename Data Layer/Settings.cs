using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    [Serializable]
    public class Settings
    {
        #region Constants


        #region GROUP AMOUNT QUESTIONS
        public const int DEFAULT_GROUP_AMOUNT_QUESTIONS = 10;
        public const int MAX_GROUP_AMOUNT_QUESTIONS = 50;
        public const int MIN_GROUP_AMOUNT_QUESTIONS = 3;
        #endregion

        #region GROUP TEST TIME
        public const double DEFAULT_GROUP_TIME_TO_NEXT_QUESTION = 2;
        public const double MAX_GROUP_TIME_TO_NEXT_QUESTION = 60;
        public const double MIN_GROUP_TIME_TO_NEXT_QUESTION = 0.1;
        #endregion

        #region PERSONAL TEST TIME
        public const double DEFAULT_PERSONAL_TIME_TO_NEXT_QURSTION = 2;
        public const double MAX_PERSONAL_TIME_TO_NEXT_QURSTION = 60;
        public const double MIN_PERSONAL_TIME_TO_NEXT_QURSTION = 1;
        #endregion

        #region PERSONAL MAX TEST TIME MINUTES
        public const double DEFAULT_PERSONAL_TIME_TO_TEST_END = 15;
        public const double MAX_PERSONAL_TIME_TO_TEST_END = 180;
        public const double MIN_PERSONAL_TIME_TEST_END = 1;
        #endregion

        #region PERSONAL AMOUNT QUESTIONS
        public const int DEFAULT_PERSONAL_AMOUNT_QUESTIONS = 10;
        public const int MAX_PERSONAL_AMOUNT_QUESTIONS = 200;
        public const int MIN_PERSONAL_AMOUNT_QUESTIONS = 3;
        #endregion

        #region PERSOMAL TEST MAX WRONGS
        public const int DEFAULT_PERSONAL_TEST_WRONGS = 2;
        public const int MAX_PERSONA_TEST_WRONGS = 50;
        public const int MIN_PERSONAL_TEST_WRONGS = 0;
        #endregion

        #region TEST IMAGE SIZES
        public const int DEFAULT_IMAGE_WIDTH = 200;
        public const int MAX_IMAGE_WIDTH = 500;
        public const int MIN_IMAGE_WIDTH = 100;

        public const int DEFAULT_IMAGE_HEIGHT = 200;
        public const int MAX_IMAGE_HEIGHT = 500;
        public const int MIN_IMAGE_HEIGHT = 100;
        #endregion

        #region TEST MODS
        public const string GROUP_MODE = "Групповой режим";
        public const string PERSONAL_MODE = "Персональный режим";
        public const string DEFAULT_SELECTED_MODE = GROUP_MODE;
        #endregion

        #region TEST RESULTS
        public const string TEST_TIME_OUT = "Истекло время выполнения";
        public const string TEST_SECCSESSFUL = "Тест успешно окончен";
        public const string TEST_UNSECCSESSFUL = "Тест не пройден";
        #endregion

        #region DATABASE FILE NAMES
        public const string DATA_TEST_FILE_NAME = "TestData.xml";
        public const string DATA_SETTINGS_FILE_NAME = "SettingsData.dat";
        #endregion

        #endregion

        #region Properties
        private int _groupAmountQuestions = DEFAULT_GROUP_AMOUNT_QUESTIONS;
        public int GroupAmountQuestions
        {
            get => _groupAmountQuestions;
            set
            {
                if(value <= MAX_GROUP_AMOUNT_QUESTIONS && value >= MIN_GROUP_AMOUNT_QUESTIONS )
                {
                    _groupAmountQuestions = value;
                }
            }
        }

        private double _groupTimeToNextQuestion = DEFAULT_GROUP_TIME_TO_NEXT_QUESTION;
        public double GroupTimeToNextQuestion
        {
            get => _groupTimeToNextQuestion;
            set
            {
                if(value <= MAX_GROUP_TIME_TO_NEXT_QUESTION && value >= MIN_GROUP_TIME_TO_NEXT_QUESTION )
                {
                    _groupTimeToNextQuestion = value;
                }
            }
        }

        private double _personalTimeToNextQuestion = DEFAULT_PERSONAL_TIME_TO_NEXT_QURSTION;
        public double PersonalTimeToNextQuestion
        {
            get => _personalTimeToNextQuestion;
            set
            {
                if(value <= MAX_PERSONAL_TIME_TO_NEXT_QURSTION && value >= MIN_PERSONAL_TIME_TO_NEXT_QURSTION )
                {
                    _personalTimeToNextQuestion = value;
                }
            }
        }

        private int _personalAmountQuestions = DEFAULT_PERSONAL_AMOUNT_QUESTIONS;
        public int PersonalAmountQuestions
        {
            get => _personalAmountQuestions;
            set
            {
                if(value <= MAX_PERSONAL_AMOUNT_QUESTIONS && value >= MIN_PERSONAL_AMOUNT_QUESTIONS )
                {
                    _personalAmountQuestions = value;
                }
            }
        }

        private int _imageWidth = DEFAULT_IMAGE_WIDTH;
        public int ImageWidht
        {
            get => _imageWidth;
            set
            {
                if(value <= MAX_IMAGE_WIDTH && value >= MIN_IMAGE_WIDTH )
                {
                    _imageWidth = value;
                }
            }
        }

        private int _imageHeight = DEFAULT_IMAGE_HEIGHT;
        public int ImageHeight
        {
            get => _imageHeight;
            set
            {
                if(value <= MAX_IMAGE_HEIGHT && value >= MIN_IMAGE_HEIGHT )
                {
                    _imageHeight = value;
                }
            }
        }

        private double _timeToTestEnd = DEFAULT_PERSONAL_TIME_TO_TEST_END;
        public double TimeToTestEnd
        {
            get => _timeToTestEnd;
            set
            {
                if(value <= MAX_PERSONAL_TIME_TO_TEST_END && value >= MIN_PERSONAL_TIME_TEST_END )
                {
                    _timeToTestEnd = value;
                }
            }
        }

        private int _maxTestWrongs = DEFAULT_PERSONAL_TEST_WRONGS;
        public int MaxTestWrongs
        {
            get => _maxTestWrongs;
            set
            {
                if(value <= MAX_PERSONA_TEST_WRONGS && value >= MIN_PERSONAL_TEST_WRONGS )
                {
                    _maxTestWrongs = value;
                }
            }
        }
        #endregion

        #region Methods
        public int GetAmountQuestions(string testingMode)
        {
            if ( string.IsNullOrWhiteSpace(testingMode) ) return 0;
            switch ( testingMode )
            {
                case GROUP_MODE: return GroupAmountQuestions;
                case PERSONAL_MODE: return PersonalAmountQuestions;
            }
            return 0;
        }

        private static Settings instance;
        public static Settings GetInstance()
        {
            if ( instance == null ) instance = new Settings();
            return instance;
        }
        public static void SetInstance(Settings inst)
        {
            if ( inst == null ) inst = new Settings();
            instance = inst;
        }
        #endregion
    }
}
