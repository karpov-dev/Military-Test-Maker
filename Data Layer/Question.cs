using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    public class Question
    {
        #region Properies
        public string Queston { get; set; }
        public string RightAnswer { get; set; }
        public List<string> Answers { get; set; }
        #endregion

        #region Constructors
        public Question()
        {
            Answers = new List<string>();
            for ( int i = 0; i < 4; i++ )
            {
                Answers.Add("");
            }
        }
        #endregion

        #region Methods
        public object Clone() => MemberwiseClone();
        #endregion
    }
}
