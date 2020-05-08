using Data_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layar.DAOClasses
{
    public class QuestionDAO
    {
        #region Database instance
        private DataBase _database = DataBase.GetInstance();
        #endregion

        #region Methods
        public List<Question> GetQuestions(Test test)
        {
            List<Question> testQuestions = new List<Question>();
            if ( test == null ) return testQuestions;
            foreach(Question question in test.Questions )
            {
                testQuestions.Add((Question)question.Clone());
            }
            return testQuestions;
        }
        public bool Insert(Test test, Question question)
        {
            if ( test == null || question == null ) return false;
            Test dataBaseTest = _database.GetTestByTitle(test.Title);
            if ( dataBaseTest == null ) return false;
            dataBaseTest.Questions.Add(question);
            return true;
        }
        public bool Remove(Test test, Question question)
        {
            if ( test == null || question == null ) return false;
            Test dataBaseTest = _database.GetTestByTitle(test.Title);
            if ( dataBaseTest == null ) return false;
            dataBaseTest.Questions.Remove(question);
            return true;
        }
        public bool Update(Test test, Question oldVersion , Question newVersion)
        {
            if ( test == null || oldVersion == null || newVersion == null ) return false;
            Question databaseQuestion = _database.GetQuestionByQuestion(test, oldVersion.Queston);
            if ( databaseQuestion == null ) return false;
            databaseQuestion = null;
            databaseQuestion = newVersion;
            return true;
        }
        #endregion
    }
}
