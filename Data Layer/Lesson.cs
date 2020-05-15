using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    [Serializable]
    public class Lesson
    {
        public string Title { get; set; }
        public List<LessonVideo> Videos { get; set; }

        public Lesson()
        {
            Videos = new List<LessonVideo>();
            Videos.Add(new LessonVideo()
            {
                Title = "Test title",
                Path = "Test Path"
            });
        }
    }
}
