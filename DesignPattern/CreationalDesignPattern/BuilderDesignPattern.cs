using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.CreationalDesignPattern.BuilderDesignPattern
{
    /*
    - The Builder Design Pattern builds a complex object using many simple objects and using a step-by-step approach.
    - The Process of constructing a complex object should be generic so that the same construction process can be used 
      to create different representations of the same complex object.
    - So, the Builder Design Pattern is all about separating the construction process from its representation

    - When to use the Builder Design Pattern in real-time applications?
        - When you want to make a complex object by specifying only its type and content. 
          The built object is constructed from the details of its construction.
        - When you decouple the process of building a complex object from the parts that make up the object.
        - When you want to isolate the code for construction and representation.
     */


    public class Report
    {
        public string ReportType { get; set; }
        public string ReportHeader { get; set; }
        public string ReportFooter { get; set; }
        public string ReportContent { get; set; }
        public void DisplayReport()
        {
            Console.WriteLine("Report Type :" + ReportType);
            Console.WriteLine("Header :" + ReportHeader);
            Console.WriteLine("Content :" + ReportContent);
            Console.WriteLine("Footer :" + ReportFooter);
        }
    }

    public abstract class ReportBuilder
    {
        protected Report reportObject;
        public abstract void SetReportType();
        public abstract void SetReportHeader();
        public abstract void SetReportContent();
        public abstract void SetReportFooter();
        public void CreateNewReport()
        {
            reportObject = new Report();
        }
        public Report GetReport()
        {
            return reportObject;
        }
    }

    class ExcelReport : ReportBuilder
    {
        public override void SetReportContent()
        {
            reportObject.ReportContent = "Excel Content Section";
        }
        public override void SetReportFooter()
        {
            reportObject.ReportFooter = "Excel Footer";
        }
        public override void SetReportHeader()
        {
            reportObject.ReportHeader = "Excel Header";
        }
        public override void SetReportType()
        {
            reportObject.ReportType = "Excel";
        }
    }

    public class PDFReport : ReportBuilder
    {
        public override void SetReportContent()
        {
            reportObject.ReportContent = "PDF Content Section";
        }
        public override void SetReportFooter()
        {
            reportObject.ReportFooter = "PDF Footer";
        }
        public override void SetReportHeader()
        {
            reportObject.ReportHeader = "PDF Header";
        }
        public override void SetReportType()
        {
            reportObject.ReportType = "PDF";
        }
    }

    public class ReportDirector
    {
        public Report MakeReport(ReportBuilder reportBuilder)
        {
            reportBuilder.CreateNewReport();
            reportBuilder.SetReportType();
            reportBuilder.SetReportHeader();
            reportBuilder.SetReportContent();
            reportBuilder.SetReportFooter();
            return reportBuilder.GetReport();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Client Code
            Report report;
            ReportDirector reportDirector = new ReportDirector();
            // Construct and display Reports
            PDFReport pdfReport = new PDFReport();
            report = reportDirector.MakeReport(pdfReport);
            report.DisplayReport();
            Console.WriteLine("-------------------");
            ExcelReport excelReport = new ExcelReport();
            report = reportDirector.MakeReport(excelReport);
            report.DisplayReport();

            Console.ReadKey();
        }
    }
}

namespace DesignPattern.CreationalDesignPattern.BuilderDesignPattern.RealTimeExample 
{
    public class Beverage
    {
        public int Water { get; set; }
        public int Milk { get; set; }
        public int Sugar { get; set; }
        public int PowderQuantity { get; set; }
        public string BeverageName { get; set; }

        public string ShowBeverage()
        {
            return "Hot " + BeverageName + " [" + Water + " ml of water, " + Milk + "ml of milk, " + Sugar
                            + " gm of sugar, " + PowderQuantity + " gm of " + BeverageName + "]\n";
        }
    }

    public abstract class BeverageBuilder
    {
        protected Beverage beverage;

        public void CreateBeverage()
        {
            beverage = new Beverage();
        }
        public Beverage GetBeverage()
        {
            return beverage;
        }

        public abstract void SetBeverageType();
        public abstract void SetWater();
        public abstract void SetMilk();
        public abstract void SetSugar();
        public abstract void SetPowderQuantity();
    }

    public class CoffeeBuilder : BeverageBuilder
    {
        public override void SetWater()
        {
            Console.WriteLine("Step 1 : Boiling water");
            GetBeverage().Water = 40;
        }
        public override void SetMilk()
        {
            Console.WriteLine("Step 2 : Adding milk");
            GetBeverage().Milk = 50;
        }
        public override void SetSugar()
        {
            Console.WriteLine("Step 3 : Adding Sugar");
            GetBeverage().Sugar = 10;
        }
        public override void SetPowderQuantity()
        {
            Console.WriteLine("Step 4 : Adding 15 Grams of coffee powder");
            GetBeverage().PowderQuantity = 15;
        }
        public override void SetBeverageType()
        {
            Console.WriteLine("Coffee");
            GetBeverage().BeverageName = "Coffee";
        }
    }

    public class TeaBuider : BeverageBuilder
    {
        public override void SetWater()
        {
            Console.WriteLine("Step 1 : Boiling water");
            GetBeverage().Water = 50;
        }
        public override void SetMilk()
        {
            Console.WriteLine("Step 2 : Adding milk");
            GetBeverage().Milk = 60;
        }
        public override void SetSugar()
        {
            Console.WriteLine("Step 3 : Adding Sugar");
            GetBeverage().Sugar = 15;
        }
        public override void SetPowderQuantity()
        {
            Console.WriteLine("Step 4 : Adding 20 Grams of tea powder");
            GetBeverage().PowderQuantity = 20;
        }
        public override void SetBeverageType()
        {
            Console.WriteLine("Tea");
            GetBeverage().BeverageName = "Tea";
        }
    }

    public class BeverageDirector
    {
        public Beverage MakeBeverage(BeverageBuilder beverageBuilder)
        {
            beverageBuilder.CreateBeverage();
            beverageBuilder.SetBeverageType();
            beverageBuilder.SetWater();
            beverageBuilder.SetMilk();
            beverageBuilder.SetSugar();
            beverageBuilder.SetPowderQuantity();

            return beverageBuilder.GetBeverage();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Beverage beverage;
            BeverageDirector beverageDirector = new BeverageDirector();

            TeaBuider tea = new TeaBuider();
            beverage = beverageDirector.MakeBeverage(tea);
            Console.WriteLine(beverage.ShowBeverage());
            CoffeeBuilder coffee = new CoffeeBuilder();
            beverage = beverageDirector.MakeBeverage(coffee);
            Console.WriteLine(beverage.ShowBeverage());
            Console.ReadKey();
        }
    }
}
