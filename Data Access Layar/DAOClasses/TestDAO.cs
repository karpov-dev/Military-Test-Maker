using Data_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layar.DAOClasses
{
    public class TestDAO
    {
        #region Database instance
        DataBase _database = DataBase.GetInstance();
        #endregion

        #region Methods
        public List<Test> GetTests()
        {
            List<Test> tests = new List<Test>();
            foreach(Test test in _database.Tests )
            {
                tests.Add((Test)test.Clone());
            }
            return tests;
        }
        public List<Question> GetRandomQuestions(Test test, int amountQuestions)
        {
            List<Question> randomTests = new List<Question>();
            if ( test == null ) return randomTests;
            if ( amountQuestions > test.Questions.Count ) return randomTests;

            List<int> randomNumbers = GetListWithRandomNumbers(amountQuestions, 0, test.Questions.Count - 1);
            for(int i = 0; i < randomNumbers.Count; i++ )
            {
                try
                {
                    randomTests.Add(test.Questions[randomNumbers[i]]);
                }
                catch
                {
                    return new List<Question>();
                }
            }

            for(int i = 0; i < randomTests.Count; i++ )
            {
                List<int> inSwap = GetListWithRandomNumbers(4, 0, 3);
                List<int> toSwap = GetListWithRandomNumbers(4, 0, 3);
                for(int j = 0; j < inSwap.Count; j++ )
                {
                    SwapAnswers(randomTests[i], inSwap[j], toSwap[j]);
                }
            }

            return randomTests;
        }
        public bool Insert(Test test)
        {
            if ( test == null ) return false;
            _database.Tests.Add(test);
            return true;
        }
        public bool Upsert(Test oldVersion, Test newVersion)
        {
            if (newVersion == null) return false;
            Test databaseTest = null;
            if (oldVersion != null) databaseTest = _database.GetTestByTitle(oldVersion.Title);

            if (databaseTest == null )
            {
                _database.Tests.Add(newVersion);
            }
            else
            {
                Remove(oldVersion);
                Insert(newVersion);
            }
            return true;
        }
        public bool Remove(Test test)
        {
            if ( test == null ) return false;
            _database.Tests.Remove(_database.GetTestByTitle(test.Title));
            return true;
        }

        private bool IsRandomNumber(List<int> numbers, int number)
        {
            foreach(int num in numbers )
            {
                if ( num == number ) return false;
            }
            return true;
        }
        private List<int> GetListWithRandomNumbers(int listSize, int minValue, int maxValue)
        {
            List<int> randomNumbers = new List<int>();
            while ( randomNumbers.Count != listSize )
            {
                int randomNumber = CustomRandom.GetNumber(minValue, maxValue);
                if ( !IsRandomNumber(randomNumbers, randomNumber) ) continue;
                randomNumbers.Add(randomNumber);
            }
            return randomNumbers;
        }
        private void SwapAnswers(Question question, int inSwap, int toSwap)
        {
            string tmpStr = question.Answers[inSwap];
            question.Answers[inSwap] = question.Answers[toSwap];
            question.Answers[toSwap] = tmpStr;
        }
        #endregion
    }
}
