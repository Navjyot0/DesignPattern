using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.CreationalDesignPattern.PrototypeDesignPattern
{
    /*
     - Definition: “Prototype Design Pattern specifies the kind of objects to create using a prototypical instance, 
                 and create new objects by copying this prototype”.
     - the Prototype Design Pattern gives us a way to create new objects from the existing instance of the object.
     - That means it clone the existing object with its data into a new object.
     - If we do any changes to the cloned object (i.e. new object) then it does not affect the original object.
     - Note: The Prototype Design Pattern is unique among the other creational design patterns as it doesn’t require a class 
             instead it requires an end object.

     - Points to Remember:
        - The MemberwiseClone method is part of the System.Object class and creates a shallow copy of the given object. 
        - MemberwiseClone Method only copies the non-static fields of the object to the new object
        - In the process of copying, if a field is a value type, a bit by bit copy of the field is performed. 
          If a field is a reference type, the reference is copied but the referenced object is not.
     */
    //public class Employee
    //{
    //    public string Name { get; set; }
    //    public string Department { get; set; }
    //}

    public class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public Employee GetClone()
        {
            return (Employee)this.MemberwiseClone();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Employee emp1 = new Employee();
            //emp1.Name = "Navjyot";
            //emp1.Department = "IT";
            //Employee emp2 = emp1;
            //emp2.Name = "Rushikesh";
            //Console.WriteLine("Emplpyee 1: ");
            //Console.WriteLine("Name: " + emp1.Name + ", Department: " + emp1.Department);
            //Console.WriteLine("Emplpyee 2: ");
            //Console.WriteLine("Name: " + emp2.Name + ", Department: " + emp2.Department);


            Employee emp1 = new Employee();
            emp1.Name = "Anurag";
            emp1.Department = "IT";
            Employee emp2 = emp1.GetClone();
            emp2.Name = "Pranaya";

            Console.WriteLine("Emplpyee 1: ");
            Console.WriteLine("Name: " + emp1.Name + ", Department: " + emp1.Department);
            Console.WriteLine("Emplpyee 2: ");
            Console.WriteLine("Name: " + emp2.Name + ", Department: " + emp2.Department);
            Console.Read();
        }
    }
    
}
