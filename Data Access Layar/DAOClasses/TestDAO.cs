using Business_Layar;
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
        #endregion
    }
}
