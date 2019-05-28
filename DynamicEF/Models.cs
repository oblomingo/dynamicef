using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicEF
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
    }

    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }

    public class Test
    {
        public int TestId { get; set; }
        public string Name { get; set; }
    }

    public class Lesson
    {
        public int LessonId { get; set; }
        public string Title { get; set; }
    }
}
