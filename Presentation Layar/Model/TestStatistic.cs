﻿using Presentation_Layar.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation_Layar.Model
{
    class TestStatistic : ModelPropertyChanged
    {
        private int _amountQuestions;
        public int AmountQuestions
        {
            get => _amountQuestions;
            set
            {
                _amountQuestions = value;
                OnPropertyChanged();
            }
        }

        private int _rightAnswer;
        public int RightQuestions
        {
            get => _rightAnswer;
            set
            {
                _rightAnswer = value;
                OnPropertyChanged();
            }
        }

        private int _currentQuestion;
        public int CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                _currentQuestion = value;
                OnPropertyChanged();
            }
        }
    }
}
