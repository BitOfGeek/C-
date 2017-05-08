using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTeacher
{
    public class Teacher
    {    public Teacher(String teacherName, int teacherEGN, String department, String gender)
        {
            this.teacherName = teacherName;
            this.teacherEGN = teacherEGN;
            this.department = department;
            this.gender = gender;
        }

        public String teacherName { get; set; }
        public int teacherEGN { get; set; }
        public String department { get; set; }
        public String gender { get; set; }
    }
}
