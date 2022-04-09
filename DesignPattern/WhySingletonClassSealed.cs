using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.WhySingletonClassSealed.WithoutSealed
{
    public class Singleton
    {
        private static int counter = 0;
        private static Singleton instance = null;
        public static Singleton GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new Singleton();
                return instance;
            }
        }
        private Singleton()
        {
            counter++;
            Console.WriteLine("Counter Value " + counter.ToString());
        }
        public void PrintDetails(string message)
        {
            Console.WriteLine(message);
        }
    }

    //Severity	Code	Description	Project	File	Line	Suppression State
    //Error CS0122  'Singleton.Singleton()' is inaccessible due to its protection level DesignPattern D:\dotNetFun\DesignPattern\DesignPattern\WhySingletonClassSealed.cs	32	Active

    //public class DerivedSingleton : Singleton
    //{
    //}
}

namespace DesignPattern.WhySingletonClassSealed.WithoutSealed.WithChildClass
{
    public class Singleton
    {
        private static int counter = 0;
        private static Singleton instance = null;
        public static Singleton GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new Singleton();
                return instance;
            }
        }
        private Singleton()
        {
            counter++;
            Console.WriteLine("Counter Value " + counter.ToString());
        }
        public void PrintDetails(string message)
        {
            Console.WriteLine(message);
        }
        public class DerivedSingleton : Singleton
        {
            //it worked but now we can ctrate multiple instances of Singleton Class
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Singleton fromTeachaer = Singleton.GetInstance;
            fromTeachaer.PrintDetails("From Teacher");
            Singleton fromStudent = Singleton.GetInstance;
            fromStudent.PrintDetails("From Student");

            /*
             * Instantiating singleton from a derived class. 
             * This violates singleton pattern principles.
             */
            Singleton.DerivedSingleton derivedObj = new Singleton.DerivedSingleton();
            derivedObj.PrintDetails("From Derived");

            Console.ReadLine();
        }
    }
}

namespace DesignPattern.WhySingletonClassSealed.WithSealed.WithChildClass
{
    public sealed class Singleton
    {
        private static int counter = 0;
        private static Singleton instance = null;
        public static Singleton GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new Singleton();
                return instance;
            }
        }
        private Singleton()
        {
            counter++;
            Console.WriteLine("Counter Value " + counter.ToString());
        }
        public void PrintDetails(string message)
        {
            Console.WriteLine(message);
        }

        //Severity Code    Description Project File Line    Suppression State
        //Error CS0509	'Singleton.DerivedSingleton': cannot derive from sealed type 'Singleton'	DesignPattern D:\dotNetFun\DesignPattern\DesignPattern\WhySingletonClassSealed.cs	116	Active

        //public class DerivedSingleton : Singleton
        //{
        //}
    }
}

/*
 So from this point, we conclude that the private constructor in c# will help us only preventing any external instantiations of the class 
 and the sealed keyword will prevent the class inheritances.
 */