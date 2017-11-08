using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWeek
{
    public class Student
    {

        public string Name { get { return name; } set { name = value; } }
        private string name;

        public string Klass { get { return klass;} set { klass = value; } }
        private string klass;

        public int Points { get { return points; } set { points = value; } }
        private int points;

        public Student(string name, string klass, int points)
        {
            this.name = name;
            this.klass = klass;
            this.points = points;
        }
    }
}
