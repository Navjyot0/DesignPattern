using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.CreationalDesignPattern.FactoryDesignPattern
{
    /*
        - A factory is an object which is used for creating other objects
        - In technical terms, we can say that a factory is a class with a method
        - In the Factory Design pattern, we create an object without exposing the object creation logic to the client 
          and the client will refer to the newly created object using a common interface.
     */

    public interface CreditCard
    {
        string GetCardType();
        int GetCreditLimit();
        int GetAnnualCharge();
    }

    class MoneyBack : CreditCard
    {
        public string GetCardType()
        {
            return "MoneyBack";
        }
        public int GetCreditLimit()
        {
            return 15000;
        }
        public int GetAnnualCharge()
        {
            return 500;
        }
    }

    public class Titanium : CreditCard
    {
        public string GetCardType()
        {
            return "Titanium Edge";
        }
        public int GetCreditLimit()
        {
            return 25000;
        }
        public int GetAnnualCharge()
        {
            return 1500;
        }
    }

    public class Platinum : CreditCard
    {
        public string GetCardType()
        {
            return "Platinum Plus";
        }
        public int GetCreditLimit()
        {
            return 35000;
        }
        public int GetAnnualCharge()
        {
            return 2000;
        }
    }


    class CreditCardFactory
    {
        public static CreditCard GetCreditCard(string cardType)
        {
            CreditCard cardDetails = null;
            if (cardType == "MoneyBack")
            {
                cardDetails = new MoneyBack();
            }
            else if (cardType == "Titanium")
            {
                cardDetails = new Titanium();
            }
            else if (cardType == "Platinum")
            {
                cardDetails = new Platinum();
            }
            return cardDetails;
        }
    }


    class Program
    {

        public static void ClientMethodWithoutFactoryPattern(string cardType)
        {
            //Generally we will get the Card Type from UI.
            //Here we are hardcoded the card type
            //string cardType = "MoneyBack";
            CreditCard cardDetails = null;
            //Based of the CreditCard Type we are creating the
            //appropriate type instance using if else condition
            if (cardType == "MoneyBack")
            {
                cardDetails = new MoneyBack();
            }
            else if (cardType == "Titanium")
            {
                cardDetails = new Titanium();
            }
            else if (cardType == "Platinum")
            {
                cardDetails = new Platinum();
            }
            if (cardDetails != null)
            {
                Console.WriteLine("CardType : " + cardDetails.GetCardType());
                Console.WriteLine("CreditLimit : " + cardDetails.GetCreditLimit());
                Console.WriteLine("AnnualCharge :" + cardDetails.GetAnnualCharge());
            }
            else
            {
                Console.Write("Invalid Card Type");
            }
        }

        public static void ClientMethodWithFactoryPattern(string cardType)
        {
            /*
                Problem of the above code implementation
                - First, the tight coupling between the client class (Program) and Product Class (MoneyBack, Titanium, and Platinum)
                - Secondly, if we add a new Credit Card, then also we need to modify the client method by adding an extra if-else
             */
            CreditCard cardDetails = CreditCardFactory.GetCreditCard(cardType);

            if (cardDetails != null)
            {
                Console.WriteLine("CardType : " + cardDetails.GetCardType());
                Console.WriteLine("CreditLimit : " + cardDetails.GetCreditLimit());
                Console.WriteLine("AnnualCharge :" + cardDetails.GetAnnualCharge());
            }
            else
            {
                Console.Write("Invalid Card Type");
            }
        }

        static void Main(string[] args)
        {
            string cardType = "MoneyBack";
            Console.WriteLine("============Without Factory============");
            ClientMethodWithoutFactoryPattern(cardType); //Without Factory
            Console.WriteLine("============With Factory============");
            ClientMethodWithFactoryPattern(cardType); //With Factory
            Console.ReadLine();
        }

        /*
            - Problems with Factory Pattern
                1. If we need to add any new product (i.e. new credit card) then we need to add a new if else condition
                2. We also have a tight coupling between the Factory (CreditCardFactory) class and product classes (MoneyBack, Titanium, and Platinum).
         */
    }
}
