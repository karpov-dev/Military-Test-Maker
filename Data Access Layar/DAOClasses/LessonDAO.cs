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
    }
}
