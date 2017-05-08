using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTeacher
{
    class StudentTeacherFunctions
    {
        public StudentTeacherFunctions()
        {
        }

       // private static List<Student> students = new List<Student>();
       // private static List<Teacher> teachers = new List<Teacher>();
        public static Dictionary<int, Student> students = new Dictionary<int, Student>();
        public static Dictionary<int, Teacher> teachers = new Dictionary<int, Teacher>();
        public static bool check = true;

        public static bool CheckInput(String input)
        {
            check = true;
            int value;
            if (!int.TryParse(input, out value))
            {
                check = false;
            }
            else if (input.Length != 10)
            {
                check = false;
            }
            return check;
        }
        public static bool KeyExistsStudent(int key)
        {
            check = true;
            if (!students.ContainsKey(key))
            {
                check = false;
            }
            return check;
        }
        public static bool KeyExistsTeacher(int key)
        {
            check = true;
            if (!teachers.ContainsKey(key))
            {
                check = false;
            }
            return check;
        }
        public static void AddNewStudent(String studentName, int facNum, int studentEGN, String faculty, String specialty, String gender)
        {
            Student student = new Student(studentName, facNum, studentEGN, faculty, specialty, gender);
            students.Add(studentEGN, student);
        }

        public static void AddNewTeacher(String teacherName, int teacherEGN, String department, String gender)
        {
            Teacher teacher = new Teacher(teacherName, teacherEGN, department, gender);
            teachers.Add(teacherEGN, teacher);
        }

        public static Student ViewStudents(int key)
        {
            return students[key];
        }

        public static void ChangeStudent(int key, Student student)
        {
            students[key] = student;
        }

        public static void RemoveStudent(int key)
        {
            students.Remove(key);
        }

        public static Teacher ViewTeachers(int key)
        {
            return teachers[key];
        }
        public static void ChangeTeacher(int key, Teacher teacher)
        {
            teachers[key] = teacher;
        }

        public static void RemoveTeacher(int key)
        {
            teachers.Remove(key);
        }
    }
}
