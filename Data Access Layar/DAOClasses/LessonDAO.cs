using Data_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layar.DAOClasses
{
    public class LessonDAO
    {
        private DataBase dataBase = DataBase.GetInstance();

        public List<Lesson> GetLessons()
        {
            return dataBase.Lessons;
        }
        public bool Insert(Lesson lesson)
        {
            if ( lesson == null ) return false;

            dataBase.Lessons.Add(lesson);
            return true;
        }
        public bool Remove(Lesson lesson)
        {
            if ( lesson == null ) return false;

            dataBase.Lessons.Remove(lesson);
            return true;
        }
        public bool InsertVideo(Lesson lesson, string title, string path)
        {
            if ( string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(path) || lesson == null) return false;

            lesson.Videos.Add(new LessonVideo()
            {
                Title = title,
                Path = path
            });
            return true;
        }
        public bool RemoveVideo(Lesson lesson, LessonVideo lessonVideo)
        {
            if ( lesson == null || lessonVideo == null ) return false;

            lesson.Videos.Remove(lessonVideo);
            return true;
        }
    }
}
