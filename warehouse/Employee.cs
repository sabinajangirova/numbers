using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse
{
    public class Employee
    {
        private string FullName { get; }
        private string Position { get; }

        public Employee(string fullName, string position)
        {
            FullName = fullName;
            Position = position;
        }
    }
}
