using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.CreationalDesignPattern.AbstractFactoryDesignPattern
{
    /*
        - Definition: “The Abstract Factory Design Pattern provides a way to encapsulate a group of individual 
                       factories that have a common theme without specifying their concrete classes“.

        - In simple words we can say, the Abstract Factory is a super factory that creates other factories. 
        - This Abstract Factory is also called the Factory of Factories.


        - Pointe to Remember:
            - Abstract Factory Pattern provides an interface for creating families of 
              related dependent objects without specifying their concrete classes.
            - The Abstract Factory Pattern provides a way to encapsulate a group of individual 
              factories that have a common theme without specifying their concrete classes.
            - The abstract factory design pattern is merely an extension to the factory method 
              pattern or factory pattern, which allows you to create objects without being concerned 
              about the actual class of the object being created.
            - Abstract means hiding some information and factory means which produces the products 
              and pattern means a design. So, the Abstract Factory Pattern is a software design pattern 
              that provides a way to encapsulate a group of individual factories that have a common theme.

        - When to use it Abstract Factory Design Pattern?
            - When you want to create a set of related objects or dependent objects which must be used together.
            - When the system should configure to work with multiple families of products.
            - When the Concrete classes should be decoupled from the clients.

        - Differences between Abstract Factory and Factory Method Design Pattern:
            - Abstract Factory Design Pattern adds a layer of abstraction to the Factory Method Design Pattern
            - The Abstract Factory design pattern implementation can have multiple factory methods
            - Similar products of a factory implementation are grouped in the Abstract factory
            - The Abstract Factory Pattern uses object composition to decouple applications from specific implementations
            - The Factory Method Pattern uses inheritance to decouple applications from specific implementations
     */

    public interface Animal
    {
        string speak();
    }

    public class Cat : Animal
    {
        public string speak()
        {
            return "Meow Meow Meow";
        }
    }

    public class Lion : Animal
    {
        public string speak()
        {
            return "Roar";
        }
    }

    public class Dog : Animal
    {
        public string speak()
        {
            return "Bark bark";
        }
    }

    public class Octopus : Animal
    {
        public string speak()
        {
            return "SQUAWCK";
        }
    }

    public class Shark : Animal
    {
        public string speak()
        {
            return "Cannot Speak";
        }
    }

    public class LandAnimalFactory : AnimalFactory
    {
        public override Animal GetAnimal(string AnimalType)
        {
            if (AnimalType.Equals("Dog"))
            {
                return new Dog();
            }
            else if (AnimalType.Equals("Cat"))
            {
                return new Cat();
            }
            else if (AnimalType.Equals("Lion"))
            {
                return new Lion();
            }
            else
                return null;
        }
    }

    public class SeaAnimalFactory : AnimalFactory
    {
        public override Animal GetAnimal(string AnimalType)
        {
            if (AnimalType.Equals("Shark"))
            {
                return new Shark();
            }
            else if (AnimalType.Equals("Octopus"))
            {
                return new Octopus();
            }
            else
                return null;
        }
    }

    public abstract class AnimalFactory
    {
        public abstract Animal GetAnimal(string AnimalType);
        public static AnimalFactory CreateAnimalFactory(string FactoryType)
        {
            if (FactoryType.Equals("Sea"))
                return new SeaAnimalFactory();
            else
                return new LandAnimalFactory();
        }
    }

    

    class Program
    {
        static void Main(string[] args)
        {
            Animal animal = null;
            AnimalFactory animalFactory = null;
            string speakSound = null;
            // Create the Sea Factory object by passing the factory type as Sea
            animalFactory = AnimalFactory.CreateAnimalFactory("Sea");
            Console.WriteLine("Animal Factory type : " + animalFactory.GetType().Name);
            Console.WriteLine();
            // Get Octopus Animal object by passing the animal type as Octopus
            animal = animalFactory.GetAnimal("Octopus");
            Console.WriteLine("Animal Type : " + animal.GetType().Name);
            speakSound = animal.speak();
            Console.WriteLine(animal.GetType().Name + " Speak : " + speakSound);
            Console.WriteLine();
            Console.WriteLine("--------------------------");
            // Create Land Factory object by passing the factory type as Land
            animalFactory = AnimalFactory.CreateAnimalFactory("Land");
            Console.WriteLine("Animal Factory type : " + animalFactory.GetType().Name);
            Console.WriteLine();
            // Get Lion Animal object by passing the animal type as Lion
            animal = animalFactory.GetAnimal("Lion");
            Console.WriteLine("Animal Type : " + animal.GetType().Name);
            speakSound = animal.speak();
            Console.WriteLine(animal.GetType().Name + " Speak : " + speakSound);
            Console.WriteLine();
            // Get Cat Animal object by passing the animal type as Cat
            animal = animalFactory.GetAnimal("Cat");
            Console.WriteLine("Animal Type : " + animal.GetType().Name);
            speakSound = animal.speak();
            Console.WriteLine(animal.GetType().Name + " Speak : " + speakSound);
            Console.Read();
        }
    }
}
