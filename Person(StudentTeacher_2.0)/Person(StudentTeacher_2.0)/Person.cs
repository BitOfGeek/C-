using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person_StudentTeacher_2._0_
{
    class Person
    {
      
        public Person()
            : this(null, 0, null)
        {   
                       
        }

        public Person(string name, int egn, string gender)
        {
            this.Name = name;
            this.EGN = egn;
            this.Gender = gender;
        }

        public string Name { get; private set; }
        public int EGN { get; private set; }
        public string Gender { get; private set; }
    }
}
