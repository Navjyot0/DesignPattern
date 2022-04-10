using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.DependencyInjectionDesignPattern
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }


    public class EmployeeBLWithoutDI
    {
        public EmployeeDAL employeeDAL;
        public List<Employee> GetAllEmployees()
        {
            employeeDAL = new EmployeeDAL();
            return employeeDAL.SelectAllEmployees();
        }
    }
    //Advantages of Constructor Dependency Injection
    //- The Constructor Dependency Injection Design Pattern makes a strong dependency contract
    //- This design pattern support testing as the dependencies are passed through the constructor.
    public class EmployeeBLWithConstructorDI
    {
        public IEmployeeDAL employeeDAL;
        public EmployeeBLWithConstructorDI(IEmployeeDAL employeeDAL)
        {
            this.employeeDAL = employeeDAL;
        }
        public List<Employee> GetAllEmployees()
        {
            return employeeDAL.SelectAllEmployees();
        }
    }
    /*
    
     What is Property Dependency Injection in C#?
    - In Property Dependency Injection, we need to supply the dependency object through a public property of the client class.

     When to use Property Dependency Injection over Constructor Injection and vice versa?
    - It ensures that all the dependency objects are initialized before we are going to invoke any methods or properties of 
      the dependency object, as a result, it avoids the null reference exceptions.
    - The Setter/Property Dependency Injection in C# is rarely used in real-time applications.
    - need to create a new method within the same class but that new method now depends on another object. 
      If we use the constructor dependency injection here, then we need to change all the existing constructor calls 
      where we created this class object. This can be a very difficult task if the project is a big one. Hence, 
      in such scenarios, the Setter or Property Dependency Injection can be a good choice.
     */
    public class EmployeeBLWithPropertyDI
    {
        private IEmployeeDAL employeeDAL;
        public IEmployeeDAL employeeDataObject
        {
            set
            {
                this.employeeDAL = value;
            }
            get
            {
                if (employeeDataObject == null)
                {
                    throw new Exception("Employee is not initialized");
                }
                else
                {
                    return employeeDAL;
                }
            }
        }
        public List<Employee> GetAllEmployees()
        {
            return employeeDAL.SelectAllEmployees();
        }
    }

    /*
     What is Method Dependency Injection in C#?
    - In Method Dependency Injection, we need to supply the dependency object through a public method of the client class.

     */

    public class EmployeeBLWithMethodDI
    {
        public IEmployeeDAL employeeDAL;

        public List<Employee> GetAllEmployees(IEmployeeDAL _employeeDAL)
        {
            employeeDAL = _employeeDAL;
            return employeeDAL.SelectAllEmployees();
        }
    }

    public interface IEmployeeDAL
    {
        List<Employee> SelectAllEmployees();
    }

    public class EmployeeDAL : IEmployeeDAL
    {
        public List<Employee> SelectAllEmployees()
        {
            List<Employee> ListEmployees = new List<Employee>();
            //Get the Employees from the Database
            //for now we are hard coded the employees
            ListEmployees.Add(new Employee() { ID = 1, Name = "Pranaya", Department = "IT" });
            ListEmployees.Add(new Employee() { ID = 2, Name = "Kumar", Department = "HR" });
            ListEmployees.Add(new Employee() { ID = 3, Name = "Rout", Department = "Payroll" });
            return ListEmployees;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Constructor DI Client Code
            EmployeeBLWithConstructorDI employeeBLWithConstructorDI = new EmployeeBLWithConstructorDI(new EmployeeDAL());
            //List<Employee> ListEmployee = employeeBLWithConstructorDI.GetAllEmployees();

            //Property DI Client Code
            EmployeeBLWithPropertyDI employeeBLWithPropertyDI = new EmployeeBLWithPropertyDI();
            employeeBLWithPropertyDI.employeeDataObject = new EmployeeDAL();
            //List<Employee> ListEmployee = employeeBLWithPropertyDI.GetAllEmployees();

            //Method DI Client Code
            EmployeeBLWithMethodDI employeeBLWithMethodDI = new EmployeeBLWithMethodDI();
            List<Employee> ListEmployee = employeeBLWithMethodDI.GetAllEmployees(new EmployeeDAL());

            
            
            foreach (Employee emp in ListEmployee)
            {
                Console.WriteLine("ID = {0}, Name = {1}, Department = {2}", emp.ID, emp.Name, emp.Department);
            }

            
            
            Console.ReadKey();
        }
    }

}
