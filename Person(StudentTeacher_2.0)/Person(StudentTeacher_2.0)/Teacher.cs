using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person_StudentTeacher_2._0_
{
    class Teacher: Person
    {
        public Teacher(string name, int egn, string department, string subject, string gender)
            : base(name, egn, gender)
        {
            this.Department = department;
            this.Subject = subject;
        }

        public Teacher()
            : this(null, 0, null, null, null)
        {   
                       
        }
        public string Department { get; private set; }
        public string Subject { get; private set; }
    }
}
