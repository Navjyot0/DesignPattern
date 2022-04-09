using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*

What is Singleton Pattern?
- We need to use the Singleton Design Pattern in C# when we need to ensures that 
  only one instance of a particular class is going to be created 
  and then provide simple global access to that instance for the entire application.

Advantages of using the Singleton Pattern
- The first and most important advantage of using the singleton design pattern in C# is that it takes care of concurrent access to the shared resource. 
  That means if we are sharing a resource with multiple clients simultaneously, 
  then concurrent access to that resource is well managed by the singleton design pattern.
- It can be lazy-loaded and also has Static Initialization.
- To share common data i.e. master data and configuration data which is not changed that frequently in an application. 
  In that case, we need to cache the objects in memory.
- As it provides a single global point of access to a particular instance, so it is easy to maintain.
- To reduce the overhead of instantiating a heavy object again and again.

Implementation Guidelines of Singleton Design Pattern in C#:
- You need to declare a constructor that should be private and parameterless. 
  This is required because it is not allowed the class to be instantiated from outside the class. 
  It only instantiates from within the class.
- The class should be declared as sealed which will ensure that it cannot be inherited. 
  This is going to be useful when you are dealing with the nested class. 
  We will discuss this scenario with an example in our upcoming article.
- You need to create a private static variable that is going to hold a reference to the single created instance of the class if any.
- You also need to create a public static property/method which will return the single-created instance of the singleton class. 
  This method or property first check if an instance of the singleton class is available or not. 
  If the singleton instance is available, then it returns that singleton instance otherwise it will create an instance and then return that instance.
 
Some Real-time scenarios where you can use the Singleton Design Pattern:
Service Proxies: As we know invoking a Service API is an extensive operation in an application. 
                 The process that taking most of the time is creating the Service client in order to invoke the service API. 
                 If you create the Service proxy as Singleton then it will improve the performance of your application.

Facades: You can also create Database connections as Singleton which can improve the performance of the application.

Logs: In an application, performing the I/O operation on a file is an expensive operation. 
      If you create your Logger as Singleton then it will improve the performance of the I/O operation.

Data sharing: If you have any constant values or configuration values then you can keep these values in Singleton 
              So that these can be read by other components of the application.

Caching: As we know fetching the data from a database is a time-consuming process. 
         In your application, you can cache the master and configuration in memory which will avoid the DB calls. 
         In such situations, the Singleton class can be used to handle the caching with thread synchronization 
         in an efficient manner which drastically improves the performance of the application. 

What are the Disadvantages of using the Singleton Design Pattern in C#?
- Unit testing is very difficult because it introduces a global state into an application.
- It reduces the potential for parallelism within a program because to access the singleton instance in a multi-threaded environment, 
  you need to serialize the object by using locking.
 */
namespace DesignPattern.CreationalDesignPattern.SingletonPattern
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            Singleton fromTeachaer = Singleton.GetInstance;
            fromTeachaer.PrintDetails("From Teacher");
            Singleton fromStudent = Singleton.GetInstance;
            fromStudent.PrintDetails("From Student");
            Console.ReadLine();
        }
    }
}
