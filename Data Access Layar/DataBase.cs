using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Data_Layer;

namespace Data_Access_Layar
{
    class DataBase
    {
        #region Data File Path
        private const string DEFAULT_DATA_PATH = "data.xml";
        private string DataPath;
        #endregion

        #region Data
        public List<Test> Tests { get; set; }
        public Settings Settings { get; set; }
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
            DataPath = DEFAULT_DATA_PATH;
            ReadData();
            Tests = new List<Test>();
            Settings = new Settings();
        }
        public DataBase(string dataPath)
        {
            if ( dataPath == null ) DataPath = DEFAULT_DATA_PATH;
            DataPath = dataPath;
            Tests = new List<Test>();
            Settings = new Settings();
            ReadData();
        }
        #endregion

        #region Read/Write data
        public void ReadData()
        {
            if ( !File.Exists(DataPath) ) return;
            Tests = new List<Test>();
            Settings = new Settings();

            XDocument xdoc = XDocument.Load(DataPath);
            foreach(XElement xmlTest in xdoc.Element("Database").Elements("Test") )
            {
                Test test = new Test()
                {
                    Title = xmlTest.Element("Title").Value,
                    Author = xmlTest.Element("Author").Value,
                    Description = xmlTest.Element("Description").Value
                };
                foreach(XElement xmlQuestion in xmlTest.Elements("Question") )
                {
                    Question question = new Question()
                    {
                        Queston = xmlQuestion.Element("Question").Value,
                        
                    };
                    test.Questions.Add(question);
                }
                Tests.Add(test);
            }

        }
        public void WriteData()
        {
            if ( Tests == null || Settings == null ) return;

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
                    XElement xmlQuestion = new XElement("Question");
                    xmlQuestion.Add(new XElement("Question", question.Queston));
                    
                    xmlTest.Add(xmlQuestion);
                }
                database.Add(xmlTest);
            }
            xdoc.Add(database);
            xdoc.Save(DataPath);
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
