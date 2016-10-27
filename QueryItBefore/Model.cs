using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryItBefore
{
    public class Person
    {
        public string Name { get; set; }
    }

    public class Employee : Person
    {
        public int Id { get; set; }
        public virtual void DoWork()
        {
            Console.WriteLine("Doing some Real Work!");
        }
    }

    public class Manager :Employee
    {
        public override void DoWork()
        {
            Console.WriteLine("Creating a Meeting");
        }
    }
}
