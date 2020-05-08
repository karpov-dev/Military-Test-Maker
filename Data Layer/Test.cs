using System;
using System.Collections.Generic;

namespace Data_Layer
{
    public class Test : ICloneable
    {
        #region Properties
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
        #endregion

        #region Constructors
        public Test()
        {
            Questions = new List<Question>();
        }
        #endregion

        #region Methods
        public object Clone()
        {
            Test clonedTest = new Test();
            clonedTest = (Test) MemberwiseClone();
            clonedTest.Questions = new List<Question>();
            for ( int i = 0; i < Questions.Count; i++ )
            {
                clonedTest.Questions.Add((Question) Questions[i].Clone());
            }
            return clonedTest;
        }
        #endregion
    }
}
