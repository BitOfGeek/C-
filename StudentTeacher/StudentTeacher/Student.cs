using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTeacher
{
    public class Student
    {
        public Student(String studentName, int facNum, int studentEGN, String faculty, String specialty, String gender)
        {
            this.studentName = studentName;
            this.facNum = facNum;
            this.studentEGN = studentEGN;
            this.faculty = faculty;
            this.specialty = specialty;
            this.gender = gender;
        }

        public  String studentName { get; set; }
        public int facNum { get; set; }
        public int studentEGN { get; set; }
        public String faculty { get; set; }
        public String specialty { get; set; }
        public String gender { get; set; }
    }
}
