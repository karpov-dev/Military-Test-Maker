using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Linq;
using Data_Layer;

namespace Data_Access_Layar
{
    public class DataBase
    {
        #region Data File Path
        private const string DATA_TEST_FILE_NAME = "TestData.xml";
        private const string DATA_SETTINGS_FILE_NAME = "SettingsData.dat";
        private string DEFAULT_PATH = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        #endregion

        #region Data
        public List<Test> Tests { get; set; }
        private Settings _settings = Settings.GetInstance();
        #endregion

        #region DataBase Instance
        private static DataBase instance;
        public static DataBase GetInstance()
        {
            if ( instance == null ) instance = new DataBase();
            return instance;
        }
        #endregion

        #region Constructors
        public DataBase()
        {
            ReadData_Settings(DEFAULT_PATH, DATA_SETTINGS_FILE_NAME);
            ReadData_Tests(DEFAULT_PATH, DATA_TEST_FILE_NAME);
        }
        #endregion

        #region Read/Write data
        public void ReadData_Tests (string path = null, string fileName = null)
        {
            Tests = new List<Test>();
            if ( string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(fileName) ) return;
            string fullPath = path + "\\" + fileName;
            if ( !File.Exists(fullPath) ) fullPath = DEFAULT_PATH + "\\" + DATA_TEST_FILE_NAME;
            if ( !File.Exists(fullPath) ) return;

            XDocument xdoc = XDocument.Load(fullPath);
            foreach(XElement xmlTest in xdoc.Element("Database").Elements("Test") )
            {
                Test test = new Test()
                {
                    Title = xmlTest.Element("Title").Value,
                    Author = xmlTest.Element("Author").Value,
                    Description = xmlTest.Element("Description").Value
                };
                foreach(XElement xmlQuestion in xmlTest.Elements("Questions") )
                {
                    Question question = new Question()
                    {
                        Queston = xmlQuestion.Element("Question").Value,
                        RightAnswer = xmlQuestion.Element("RightAnswer").Value
                    };
                    question.Answers = new List<string>();
                    foreach(XElement xmlAnswer in xmlQuestion.Element("Answers").Elements("Answer") )
                    {
                        question.Answers.Add(xmlAnswer.Value);
                    }
                    test.Questions.Add(question);
                }
                Tests.Add(test);
            }

        }
        public void ReadData_Settings(string path = null, string fileName = null)
        {
            if ( string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(fileName) ) return;
            string fullPath = path + "\\" + fileName;
            if ( !File.Exists(fullPath) ) fullPath = DEFAULT_PATH + "\\" + DATA_SETTINGS_FILE_NAME;
            if ( !File.Exists(fullPath) ) return;

            BinaryFormatter formatter = new BinaryFormatter();

            using ( FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate) )
            {
                Settings.SetInstance((Settings) formatter.Deserialize(fs));
            }
        }

        public void WriteData_Tests(string path = null, string fileName = null)
        {
            if ( Tests == null ) return;
            string fullName = "";
            if ( string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(fileName) ) fullName = DEFAULT_PATH + "\\" + DATA_TEST_FILE_NAME;
            else fullName = path + "\\" + fileName;

            XDocument xdoc = new XDocument();
            XElement database = new XElement("Database");
            foreach(Test test in Tests )
            {
                XElement xmlTest = new XElement("Test");
                xmlTest.Add(new XElement("Title", test.Title));
                xmlTest.Add(new XElement("Author", test.Author));
                xmlTest.Add(new XElement("Description", test.Description));
                foreach(Question question in test.Questions )
                {
                    XElement xmlQuestion = new XElement("Questions");
                    xmlQuestion.Add(new XElement("Question", question.Queston));
                    xmlQuestion.Add(new XElement("RightAnswer", question.RightAnswer));
                    XElement xmlAnswers = new XElement("Answers");
                    foreach (string answer in question.Answers )
                    {
                        xmlAnswers.Add(new XElement("Answer", answer));
                    }
                    xmlQuestion.Add(xmlAnswers);
                    xmlTest.Add(xmlQuestion);
                }
                database.Add(xmlTest);
            }
            xdoc.Add(database);
            try
            {
                xdoc.Save(fullName);
            } catch(Exception e )
            {

            }
            
        }
        public void WriteData_Settings(string path = null, string fileName = null)
        {
            string fullPath = "";
            if ( string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(fileName) ) fullPath = DEFAULT_PATH + "\\" + DATA_SETTINGS_FILE_NAME;
            else fullPath = DEFAULT_PATH + "\\" + fileName;

            BinaryFormatter formatter = new BinaryFormatter();
            using ( FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate) )
            {
                formatter.Serialize(fs, _settings);
            }
        }
        #endregion

        #region Methods
        public Test GetTestByTitle(string title)
        {
            if ( string.IsNullOrWhiteSpace(title) || Tests == null) return null;

            foreach(Test test in Tests )
            {
                if ( test.Title == title ) return test;
            }

            return null;
        }
        public Question GetQuestionByQuestion(Test test, string question)
        {
            if ( string.IsNullOrWhiteSpace(question) || test == null) return null;
            Test databaseTest = this.GetTestByTitle(test.Title);
            if ( databaseTest == null ) return null;
            foreach(Question testQuestion in databaseTest.Questions )
            {
                if ( testQuestion.Queston == question ) return testQuestion;
            }
            return null;
        }
        #endregion
    }
}
