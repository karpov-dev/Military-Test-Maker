using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    [Serializable]
    public class Settings
    {
        #region Constants
        public const int DEFAULT_GROUP_AMOUNT_QUESTIONS = 10;
        public const int MAX_GROUP_AMOUNT_QUESTIONS = 50;
        public const int MIN_GROUP_AMOUNT_QUESTIONS = 3;

        public const double DEFAULT_GROUP_TIME_TO_NEXT_QUESTION = 2;
        public const double MAX_GROUP_TIME_TO_NEXT_QUESTION = 60;
        public const double MIN_GROUP_TIME_TO_NEXT_QUESTION = 0.1;

        public const double DEFAULT_PERSONAL_TIME_TO_NEXT_QURSTION = 2;
        public const double MAX_PERSONAL_TIME_TO_NEXT_QURSTION = 60;
        public const double MIN_PERSONAL_TIME_TO_NEXT_QURSTION = 0.1;

        public const int DEFAULT_PERSONAL_AMOUNT_QUESTIONS = 10;
        public const int MAX_PERSONAL_AMOUNT_QUESTIONS = 50;
        public const int MIN_PERSONAL_AMOUNT_QUESTIONS = 3;

        public const int DEFAULT_IMAGE_WIDTH = 200;
        public const int MAX_IMAGE_WIDTH = 500;
        public const int MIN_IMAGE_WIDTH = 100;

        public const int DEFAULT_IMAGE_HEIGHT = 200;
        public const int MAX_IMAGE_HEIGHT = 500;
        public const int MIN_IMAGE_HEIGHT = 100;

        public const bool DEFAULT_SHOW_RIGHT_ANSWERS = true;

        public const string GROUP_MODE = "Групповой режим";
        public const string PERSONAL_MODE = "Персональный режим";
        public const string DEFAULT_SELECTED_MODE = GROUP_MODE;
        #endregion


        #region Instance
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

        private bool _showRightAnswer = DEFAULT_SHOW_RIGHT_ANSWERS;
        public bool ShowRightAnswer
        {
            get => _showRightAnswer;
            set
            {
                _showRightAnswer = value;
            }
        }

        private double _personalTimeToAnswer = DEFAULT_PERSONAL_TIME_TO_NEXT_QURSTION;
        public double PersonalTimeToAnswer
        {
            get => _personalTimeToAnswer;
            set
            {
                if(value <= MAX_PERSONAL_TIME_TO_NEXT_QURSTION && value >= MIN_PERSONAL_TIME_TO_NEXT_QURSTION )
                {
                    _personalTimeToAnswer = value;
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
        #endregion
    }
}
