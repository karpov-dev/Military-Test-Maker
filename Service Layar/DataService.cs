using Data_Layer;
using Data_Access_Layar.DAOClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service_Layar
{
    public class DataService
    {
        #region Data Access Objects
        private QuestionDAO _questionDAO = new QuestionDAO();
        private TestDAO _testDAO = new TestDAO();
        private LessonDAO _lessonDAO = new LessonDAO();
        #endregion

        #region Instance
        private static DataService _instance;
        public static DataService GetInstance()
        {
            if ( _instance == null ) _instance = new DataService();
            return _instance;
        }
        #endregion

        #region Question DAO Methods
        public List<Question> GetQuestions(Test test) => _questionDAO.GetQuestions(test);
        public bool InsertQuestion(Test test, Question question) => _questionDAO.Insert(test, question);
        public bool RemoveQuestion(Test test, Question question) => _questionDAO.Remove(test, question);
        public bool UpdateQuestion(Test test, Question oldVersion, Question newVersion) => _questionDAO.Update(test, oldVersion, newVersion);
        #endregion

        #region Test DAO Methods
        public List<Test> GetTests() => _testDAO.GetTests();
        public List<Question> GetRandomQuestionsByTest(Test test, int amountQuestion) => _testDAO.GetRandomQuestions(test, amountQuestion);
        public bool InsertTest(Test test) => _testDAO.Insert(test);
        public bool RemoveTest(Test test) => _testDAO.Remove(test);
        public bool UpsertTest(Test oldVersion, Test newVersion) => _testDAO.Upsert(oldVersion, newVersion);
        #endregion

        #region Lesson DAO Methods 
        public List<Lesson> GetLessons() => _lessonDAO.GetLessons();
        public bool InsertLesson(Lesson lesson) => _lessonDAO.Insert(lesson);
        public bool RemoveLesson(Lesson lesson) => _lessonDAO.Remove(lesson);
        #endregion
    }
}
