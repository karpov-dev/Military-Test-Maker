using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    [Serializable]
    public class Lesson
    {
        public string Title { get; set; }
        public Dictionary<string, string> Videos { get; set; }

        public Lesson()
        {
            Videos = new Dictionary<string, string>();
            Title = "Test";
            Videos.Add("Test Key", "Test Value");
        }
    }
}
