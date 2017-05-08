using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person_StudentTeacher_2._0_
{
    class Student : Person
    {
        public Student(string name, int facNum, int egn, string faculty, string specialty, string gender) 
            : base(name,  egn, gender)
        {
            this.FacNum = facNum;
            this.Faculty = faculty;
            this.Specialty = specialty;
        }
        public Student()
            : this(null, 0, 0, null, null, null)
        {   
                       
        }

    
        public int FacNum { get; private set; }
        public string Faculty { get; private set; }
        public string Specialty { get; private set; }
    }
}
