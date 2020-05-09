using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    public static class Settings
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

        public const bool DEFAULT_SHOW_RIGHT_ANSWERS = true;

        public const string GROUP_MODE = "Групповой режим";
        public const string PERSONAL_MODE = "Персональный режим";
        public const string DEFAULT_SELECTED_MODE = GROUP_MODE;
        #endregion

        #region Properties
        private static int _groupAmountQuestions = DEFAULT_GROUP_AMOUNT_QUESTIONS;
        public static int GroupAmountQuestions
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

        private static double _groupTimeToNextQuestion = DEFAULT_GROUP_TIME_TO_NEXT_QUESTION;
        public static double GroupTimeToNextQuestion
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

        private static bool _showRightAnswer = DEFAULT_SHOW_RIGHT_ANSWERS;
        public static bool ShowRightAnswer
        {
            get => _showRightAnswer;
            set
            {
                _showRightAnswer = value;
            }
        }

        private static double _personalTimeToAnswer = DEFAULT_PERSONAL_TIME_TO_NEXT_QURSTION;
        public static double PersonalTimeToAnswer
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

        private static int _personalAmountQuestions = DEFAULT_PERSONAL_AMOUNT_QUESTIONS;
        public static int PersonalAmountQuestions
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
        #endregion

        #region Methods
        public static int GetAmountQuestions(string testingMode)
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
