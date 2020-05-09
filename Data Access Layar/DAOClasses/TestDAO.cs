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

            List<int> randomNumbers = new List<int>();
            while( randomNumbers.Count != amountQuestions )
            {
                int randomNumber = CustomRandom.GetNumber(0, test.Questions.Count - 1);
                if ( !IsRandomNumber(randomNumbers, randomNumber) ) continue;
                randomNumbers.Add(randomNumber);
            }
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
        #endregion
    }
}
