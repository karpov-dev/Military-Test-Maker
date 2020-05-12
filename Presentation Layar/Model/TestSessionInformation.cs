using Presentation_Layar.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation_Layar.Model
{
    class TestSessionInformation : ModelPropertyChanged
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

        private int _wrongQestions;
        public int WrongQuestions
        {
            get => _wrongQestions;
            set
            {
                _wrongQestions = value;
                OnPropertyChanged();
            }
        }

        private string _testResult;
        public string TestResult
        {
            get => _testResult;
            set
            {
                _testResult = value;
                OnPropertyChanged();
            }
        }

        private int _currentMinute;
        public int CurrentMinute
        {
            get => _currentMinute;
            set
            {
                _currentMinute = value;
                OnPropertyChanged();
            }
        }

        private int _currentSecond;
        public int CurrentSecond
        {
            get => _currentSecond;
            set
            {
                _currentSecond = value;
                if(_currentSecond < 0 )
                {
                    CurrentMinute--;
                    _currentSecond = 59;
                }
                OnPropertyChanged();
            }
        }

        private double _totalTime;
        public double TotalTime
        {
            get => _totalTime;
            set
            {
                _totalTime = value;
                OnPropertyChanged();
            }
        }

        public void SetTestTime(int minutes)
        {
            if(minutes > 0 )
            {
                CurrentMinute = minutes;
            }
        }
        public void RemoveSecond()
        {
            CurrentSecond--;
            TotalTime++;
        }
    }
}
