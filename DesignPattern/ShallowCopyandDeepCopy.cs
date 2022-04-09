using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
    Shallow copy

    - In the case of Shallow copy, it will create the new object from the existing object and then copying the 
      value type fields of the current object to the new object.
    - But in the case of reference type, it will only copy the reference, not the referred object itself.
    - Therefore the original and clone refer to the same object in the case of reference type.

    Deep Copy
    - In the case of deep copy, it will create the new object from the existing object and then copying the 
      fields of the current object to the newly created object.
    - If the field is a value type, then a bit-by-bit copy of the field will be performed.
    - If the field is a reference type, then a new copy of the referred object is created.
*/
namespace DesignPattern.ShallowCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee emp1 = new Employee();
            emp1.Name = "Anurag";
            emp1.Department = "IT";
            emp1.EmpAddress = new Address() { address = "BBSR" };
            Employee emp2 = emp1.GetClone();
            emp2.Name = "Pranaya";
            emp2.EmpAddress.address = "Mumbai";
            Console.WriteLine("Emplpyee 1: ");
            Console.WriteLine("Name: " + emp1.Name + ", Address: " + emp1.EmpAddress.address + ", Dept: " + emp1.Department);
            Console.WriteLine("Emplpyee 2: ");
            Console.WriteLine("Name: " + emp2.Name + ", Address: " + emp2.EmpAddress.address + ", Dept: " + emp2.Department);
            Console.Read();
        }
    }
    public class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public Address EmpAddress { get; set; }
        public Employee GetClone()
        {
            return (Employee)this.MemberwiseClone();
        }
    }
    public class Address
    {
        public string address { get; set; }
    }
}

namespace DesignPattern.DeepCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee emp1 = new Employee();
            emp1.Name = "Anurag";
            emp1.Department = "IT";
            emp1.EmpAddress = new Address() { address = "BBSR" };
            Employee emp2 = emp1.GetClone();
            emp2.Name = "Pranaya";
            emp2.EmpAddress.address = "Mumbai";
            Console.WriteLine("Emplpyee 1: ");
            Console.WriteLine("Name: " + emp1.Name + ", Address: " + emp1.EmpAddress.address + ", Dept: " + emp1.Department);
            Console.WriteLine("Emplpyee 2: ");
            Console.WriteLine("Name: " + emp2.Name + ", Address: " + emp2.EmpAddress.address + ", Dept: " + emp2.Department);
            Console.Read();
        }
    }
    public class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public Address EmpAddress { get; set; }
        public Employee GetClone()
        {
            Employee employee = (Employee)this.MemberwiseClone();
            employee.EmpAddress = EmpAddress.GetClone();
            return employee;
        }
    }
    public class Address
    {
        public string address { get; set; }
        public Address GetClone()
        {
            return (Address)this.MemberwiseClone();
        }
    }
}
