using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 - The Adapter Design Pattern in C# works as a bridge between two incompatible interfaces.
 - Adapter Design Pattern can be implemented in two ways.
    1. Object Adapter Pattern
    2. Class Adapter Pattern
 - The Adapter Design Pattern is composed of four components.
    1. Client: The Client class can only see the Target interface.
    2. Target: It is an interface and this interface needs to be implemented by the Adapter and the client can see only this interface.
    3. Adapter: This is a class that makes two incompatible systems work together. The Adapter class implements the Trager interface 
                and provides the implementation for the ServiceA method. This class is also composed of the Adaptee 
                i.e. it has a reference to the Adaptee object.
    4. Adaptee: This class contains the functionality which the client requires but it’s not compatible with the existing client code. 
                So, it requires some adaptation before the client code can use it. 
                It means the client will call the Adapter and the Adapter will do the conversion if required and then it will make a call to the Adaptee.
 - When to use the Object Adapter pattern and when to use the Class Adapter Pattern in C#?
   It is completely based on the situation. For example, 
   if you have a java class and you want to make it compatible with the dot net class, 
   then you need to use the object adapter pattern and the reason is it is not possible to make inheritance. 
   On the other hand, if both the classes are within the same project and using the same programming language 
   and if the inheritance is possible then you need to go for Class Adapter Design Pattern.

 - When to use the Adapter Design Pattern in the real-time application?
   We need to choose the Adapter Design Pattern in real-time applications when
   - A class needs to be reused that does not have an interface that a client requires.
   - Allow a system to use classes of another system that is incompatible with it.
   - Allow communication between a new and already existing system that is independent of each other.
   - Sometimes a toolkit or class library cannot be used because its interface is incompatible with the interface required by an application.
 */

namespace DesignPattern.StructuralDesignPattern.AdapterDesignPattern.ObjectAdapterDesignPattern
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public decimal Salary { get; set; }
        public Employee(int id, string name, string designation, decimal salary)
        {
            ID = id;
            Name = name;
            Designation = designation;
            Salary = salary;
        }
    }

    //Adaptee
    public class ThirdPartyBillingSystem
    {
        //ThirdPartyBillingSystem accepts employees information as a List to process each employee salary
        public void ProcessSalary(List<Employee> listEmployee)
        {
            foreach (Employee employee in listEmployee)
            {
                Console.WriteLine("Rs." + employee.Salary + " Salary Credited to " + employee.Name + " Account");
            }
        }
    }

    //Target
    public interface ITarget
    {
        void ProcessCompanySalary(string[,] employeesArray);
    }

    //Adapter
    public class EmployeeAdapter : ITarget
    {
        ThirdPartyBillingSystem thirdPartyBillingSystem = new ThirdPartyBillingSystem();

        public void ProcessCompanySalary(string[,] employeesArray)
        {
            string Id = null;
            string Name = null;
            string Designation = null;
            string Salary = null;
            List<Employee> listEmployee = new List<Employee>();
            for (int i = 0; i < employeesArray.GetLength(0); i++)
            {
                for (int j = 0; j < employeesArray.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        Id = employeesArray[i, j];
                    }
                    else if (j == 1)
                    {
                        Name = employeesArray[i, j];
                    }
                    else if (j == 2)
                    {
                        Designation = employeesArray[i, j];
                    }
                    else
                    {
                        Salary = employeesArray[i, j];
                    }
                }
                listEmployee.Add(new Employee(Convert.ToInt32(Id), Name, Designation, Convert.ToDecimal(Salary)));
            }
            Console.WriteLine("Adapter converted Array of Employee to List of Employee");
            Console.WriteLine("Then delegate to the ThirdPartyBillingSystem for processing the employee salary\n");
            thirdPartyBillingSystem.ProcessSalary(listEmployee);
        }
    }

    //Client
    class Program
    {
        static void Main(string[] args)
        {
            string[,] employeesArray = new string[5, 4]
            {
                {"101","John","SE","10000"},
                {"102","Smith","SE","20000"},
                {"103","Dev","SSE","30000"},
                {"104","Pam","SE","40000"},
                {"105","Sara","SSE","50000"}
            };

            ITarget target = new EmployeeAdapter();
            Console.WriteLine("HR system passes employee string array to Adapter\n");
            target.ProcessCompanySalary(employeesArray);
            Console.Read();
        }
    }
}

namespace DesignPattern.StructuralDesignPattern.AdapterDesignPattern.ClassAdapterDesignPattern
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public decimal Salary { get; set; }
        public Employee(int id, string name, string designation, decimal salary)
        {
            ID = id;
            Name = name;
            Designation = designation;
            Salary = salary;
        }
    }

    //Adaptee
    public class ThirdPartyBillingSystem
    {
        //ThirdPartyBillingSystem accepts employees information as a List to process each employee salary
        public void ProcessSalary(List<Employee> listEmployee)
        {
            foreach (Employee employee in listEmployee)
            {
                Console.WriteLine("Rs." + employee.Salary + " Salary Credited to " + employee.Name + " Account");
            }
        }
    }

    //Target
    public interface ITarget
    {
        void ProcessCompanySalary(string[,] employeesArray);
    }

    //Adapter
    public class EmployeeAdapter : ThirdPartyBillingSystem, ITarget
    {
        public void ProcessCompanySalary(string[,] employeesArray)
        {
            string Id = null;
            string Name = null;
            string Designation = null;
            string Salary = null;
            List<Employee> listEmployee = new List<Employee>();
            for (int i = 0; i < employeesArray.GetLength(0); i++)
            {
                for (int j = 0; j < employeesArray.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        Id = employeesArray[i, j];
                    }
                    else if (j == 1)
                    {
                        Name = employeesArray[i, j];
                    }
                    else if (j == 1)
                    {
                        Designation = employeesArray[i, j];
                    }
                    else
                    {
                        Salary = employeesArray[i, j];
                    }
                }
                listEmployee.Add(new Employee(Convert.ToInt32(Id), Name, Designation, Convert.ToDecimal(Salary)));
            }
            Console.WriteLine("Adapter converted Array of Employee to List of Employee");
            Console.WriteLine("Then delegate to the ThirdPartyBillingSystem for processing the employee salary\n");
            ProcessSalary(listEmployee);
        }
    }

    //Client
    class Program
    {
        static void Main(string[] args)
        {
            string[,] employeesArray = new string[5, 4]
            {
                {"101","John","SE","10000"},
                {"102","Smith","SE","20000"},
                {"103","Dev","SSE","30000"},
                {"104","Pam","SE","40000"},
                {"105","Sara","SSE","50000"}
            };

            ITarget target = new EmployeeAdapter();
            Console.WriteLine("HR system passes employee string array to Adapter\n");
            target.ProcessCompanySalary(employeesArray);
            Console.Read();
        }
    }
}

namespace DesignPattern.StructuralDesignPattern.AdapterDesignPattern.RealTimeExample
{
    //Interface(Adaptee)
    public interface IEnglishSpeaker
    {
        string AskQuestion(string Words);
        string AnswerFortheQuestion(string Words);
    }
    
    // John is from USA, So he can speak and understand only English
    public class John : IEnglishSpeaker
    {
        public string AskQuestion(string Words)
        {
            Console.WriteLine("Question Asked by John [English Speaker and Can understand only English] : " + Words);
            ITarget target = new Pam();
            string replyFromDavid = target.TranslateAndTellToOtherPerson(Words, "French");
            return replyFromDavid;
        }

        public string AnswerFortheQuestion(string Words)
        {
            string reply = null;
            if (Words.Equals("where are you?", StringComparison.InvariantCultureIgnoreCase))
            {
                reply = "I am in USA";
            }
            return reply;
        }
    }

    //Interface (Adaptee)
    public interface IFrenchSpeaker
    {
        string AskQuestion(string Words);
        string AnswerFortheQuestion(string Words);
    }

    
    // David is from France and can speak and understand only French
    public class David : IFrenchSpeaker
    {
        public string AskQuestion(string Words)
        {
            Console.WriteLine("Question Asked by David [French Speaker and Can understand only French] : " + Words);
            ITarget target = new Pam();
            string replyFromJohn = target.TranslateAndTellToOtherPerson(Words, "English");
            return replyFromJohn;
        }
        public string AnswerFortheQuestion(string Words)
        {
            string reply = null;
            if (Words.Equals("comment allez-vous?", StringComparison.InvariantCultureIgnoreCase))
            {
                reply = "Je suis très bien";
            }
            return reply;
        }
    }

    //Target interface
    public interface ITarget
    {
        string TranslateAndTellToOtherPerson(string words, string convertToWhichLanguage);
    }

    //Translator or Adapter
    // Adapter or Translator
    // Pam can speak and understand both English and French
    // She acts as an Adapter or Translator
    public class Pam : ITarget
    {
        static Dictionary<string, string> EnglishFrenchDictionary = new Dictionary<string, string>();
        static Dictionary<string, string> FrenchEnglishDictionary = new Dictionary<string, string>();
        David david = new David();
        John john = new John();

        static Pam()
        {
            EnglishFrenchDictionary.Add("how are you?", "comment allez-vous?");
            EnglishFrenchDictionary.Add("I am in USA", "Je suis aux Etats-Unis");
            FrenchEnglishDictionary.Add("Je suis trC(s bien", "I am fine");
            FrenchEnglishDictionary.Add("oC9 C*tes-vous?", "where are you?");
        }
        public string TranslateAndTellToOtherPerson(string Words, string ConvertToWhichLanguage)
        {
            if (ConvertToWhichLanguage.Equals("English", StringComparison.InvariantCultureIgnoreCase))
            {
                string EnglishWords = ConvertToEnglish(Words);
                Console.WriteLine("\nPam Converted \"" + Words + " \" to \"" + EnglishWords + " and send the question to John");
                string EnglishWordsReply = john.AnswerFortheQuestion(EnglishWords);
                Console.WriteLine("Pam Got reply from John in English : " + "\"" + EnglishWordsReply + "\"");
                string FrenchConverted = ConvertToFrench(EnglishWordsReply);
                Console.WriteLine("Pam Converted " + "\"" + EnglishWordsReply + "\"" + " to " + "\"" + FrenchConverted + "\"" + " and send back to David\n");
                return FrenchConverted;
            }
            else if (ConvertToWhichLanguage.Equals("French", StringComparison.InvariantCultureIgnoreCase))
            {
                string FrenchWords = ConvertToFrench(Words);
                Console.WriteLine("\nPam Converted \"" + Words + " \" to \"" + FrenchWords + " and send the question to David");
                string FrenchWordsReply = david.AnswerFortheQuestion(FrenchWords);
                Console.WriteLine("Pam Got reply from David in French : " + "\"" + FrenchWordsReply + "\"");
                string EnglishConverted = ConvertToEnglish(FrenchWordsReply);
                Console.WriteLine("Pam Converted " + "\"" + FrenchWordsReply + "\"" + " to " + "\"" + EnglishConverted + "\"" + " and send back to John\n");
                return EnglishConverted;
            }
            else
            {
                return "Sorry Cannot Covert";
            }
        }
        public string ConvertToFrench(string Words)
        {
            return EnglishFrenchDictionary[Words];
        }
        public string ConvertToEnglish(string Words)
        {
            return FrenchEnglishDictionary[Words];
        }
    }

    //Client
    class Program
    {
        static void Main(string[] args)
        {
            string replyFromDavid = new John().AskQuestion("how are you?");
            Console.WriteLine("Reply From David [French Speaker can Speak and Understand only French] :  " + replyFromDavid);
            Console.WriteLine();
            string replyFromJohn = new David().AskQuestion("où êtes-vous?");
            Console.WriteLine("Reply From John [English Speaker can Speak and Understand only English] :  " + replyFromJohn);

            Console.Read();
        }
    }
}