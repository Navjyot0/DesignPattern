using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.CreationalDesignPattern.FluentInterfaceDesignPattern
{
    /*
     
    - The main objective of the Fluent Interface Design Pattern is that we can apply multiple properties (or methods) 
      to an object by connecting them with dots (.) without having to re-specify the object name each time.
    - When do we need to use the Fluent Interface Design Pattern in C#?
        - During UNIT testing when the developers are not full-fledged programmers.
        - When you want your code to be readable by non-programmers so that they can understand 
          if the code is satisfied with their business logic or not.
        - If you are a component seller and you want to stand out in the market as compared to the others by making your interface simpler.
    - I have seen fluent interfaces are used extensively in LINQ Queries: Searching, Sorting, pagination, grouping with a blend of LINQ
     */

    public class Employee
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
    }
    public class FluentEmployee
    {
        private Employee employee = new Employee();
        public FluentEmployee NameOfTheEmployee(string FullName)
        {
            employee.FullName = FullName;
            return this;
        }
        public FluentEmployee Born(string DateOfBirth)
        {
            employee.DateOfBirth = Convert.ToDateTime(DateOfBirth);
            return this;
        }
        public FluentEmployee WorkingOn(string Department)
        {
            employee.Department = Department;
            return this;
        }
        public FluentEmployee StaysAt(string Address)
        {
            employee.Address = Address;
            return this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FluentEmployee obj = new FluentEmployee();
            obj.NameOfTheEmployee("Navjyot")
                    .Born("05/28/1994")
                    .WorkingOn("IT")
                    .StaysAt("Pune-India");
            Console.Read();
        }
    }
}
