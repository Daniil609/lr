using System;
using System.Text;
using Microsoft.CSharp;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace dop
{
    class AllCars
    {
        Veihicle[] data;
        public AllCars(int kolvo)
        {
            data = new Veihicle[kolvo];
        }
        public Veihicle this[int index]
        {
            get
            {
                return data[index];
            }
            set
            {
                data[index] = value;
            }
        }
    }
    
    interface ThisViechicl 
    {
        string Marka { get; }

    }
    abstract class Veihicle:ThisViechicl
    {
        public string Marka { get; set; }
        protected int year;
        protected double volume;
        public int Weight { get; set; }
        protected int ID;
        public delegate void Information();
        public Veihicle() : this("UnKnown",0, 0, 0, CreateID())
        { }
        public Veihicle(string marka) : this(marka, 0, 0, 0,  CreateID())
        { }
        public Veihicle(string marka, int year, double volume) : this(marka, year, volume, 0,  CreateID())
        { }
        public Veihicle(string marka,int year, double volume, int weight ,int id) : this(marka, year, volume, weight, id)
        {
            Year = year;
            Volume = volume;
            Weight = weight;
            Marka = marka;
            ID = id;
        }

        protected Veihicle(int year, double volume, int weight, string marka)
        {
            this.year = year;
            this.volume = volume;
            Weight = weight;
            Marka = marka;
        }

        public double Volume
        {
            set
            {
                if (value >= 0 && value <= 7)
                {
                    volume = value;
                }
                else
                {
                    volume= 0;
                }
            }
            get { return volume; }
        }
        public int Year
        {
            set
            {
                if (value >= 0 && value <= 7)
                {
                    year = value;
                }
                else
                {
                    year = 0;
                }
            }
            get { return year; }
        }

        static Random rand = new Random();

        public static int CreateID()
        {
            return rand.Next(1000, 10000);
        }
        public virtual string Show()
        {
            return ($"Marka: {Marka}\nYear: {year.ToString()}\nVolume: {Volume.ToString()}\nWeight: {Weight.ToString()}");
        }
        public int CompareTo(object obj)
        {
            Car m = obj as Car;
            if (m != null)
                return ID.CompareTo(m.ID);
            throw new Exception("Невозможно сравнить эти объекты!");
        }
        public abstract string ShowNumber();

    }
    class Car : Veihicle
    {
        protected string Country;
        protected int[] NumberOfSeates;
        public int Length { get; private set; }

        public event Information Info;
        public event Information _Info
        {
            add
            {
                Info += value;
                Console.WriteLine($"\nHello, Console! {value.Method.Name} added.");
            }
            remove
            {
                Info -= value;
                Console.WriteLine($"\nHello, Console! {value.Method.Name} removed.");
            }
        }

        public string country
        {
            get
            {
                return Country;
            }
            set
            {
                if (value == "Germany" && value == "Italia")
                    Country = value;
                else
                {
                    Country = "Unknown " + value;
                    throw new ArgumentException();
                }

            }

        }
        public Car(string marka, int year, double volume, int weight, int amount, string countr) : base(year, volume, weight, marka)
        { 


            NumberOfSeates = new int[amount];
            Length = amount;
            Country = countr;
        }
        public Car(int year, int amount) : this("Porshe",year, 1.8, 1500, amount, "Germany")
        {
            NumberOfSeates = new int[amount];
            Length = amount;
        }
        public Car(int amount, string countr) : this("Porshe", 2011, 1.8, 1500, amount, countr)
        {
            NumberOfSeates = new int[amount];
            Length = amount;
            Country = countr;
        }
        public Car() : this("Porshe",2011, 1.8, 1500, 2, "Germany")
        { }
        public int this[int index]
        {
            get
            {
                if (index > 0 && index <= Length)
                    return NumberOfSeates[index - 1];
                else
                    return NumberOfSeates[0];
            }
            set
            {
                if (index > 0 && index <= Length && value <= 10 && value >= 0)
                    NumberOfSeates[index - 1] = value;
                else
                    throw new ArgumentException();
                
            }
        }
        public new virtual string Show()
        {
            Info?.Invoke();
            return $"Marka: {Marka}\nYear: {year.ToString()}\nValue: {volume.ToString()}\nWeight: {Weight.ToString()}\nCountry: {Country}";
        }
        public override string ShowNumber()
        {
            Info?.Invoke();
            string ocenki = "";
            foreach (int mark in NumberOfSeates)
                ocenki += mark.ToString() + " ";
            return ocenki;
        }


    }
    public interface IComparer<T>
    {
        int Compare(T up1, T up2);
    }
    class AgeComparer : IComparer<Veihicle>
    {
        public int Compare(Veihicle ps1, Veihicle ps2)
        {
            if (ps1.Year < ps2.Year)
            {
                Console.WriteLine($"\nThis car from {ps1.Marka} is older, than {ps2.Marka}.");
                return 1;
            }
            else
            {
                Console.WriteLine($"\nCar {ps1.Marka} is younger, than {ps2.Marka}.");
                return 0;
            }
        }
    }
    public interface IExample1
    {
        public int NamberOfMotor { get; set; }
        public string Mobel { get; set; }

    }

    sealed class Mercedes : Car, IExample1
    {
        public int NamberOfMotor { get; set; }
        private string model;

        public string Model
        {
            get
            {
                return model;
            }
            set
            {
                if (value == "sedan" && value == "universal")
                    model = value;
                else
                {
                    model = "Unknown " + value;
                    throw new ArgumentException();
                }

            }
        }
        public Mercedes( int numberOfMotor, string model1) : this("Porshe",2011, 1.8, 1500, 2, "Germany",numberOfMotor,model1)
        {
            NamberOfMotor = numberOfMotor;
            model = model1;

        }

        public Mercedes(string marka, int year, double volume, int weight, int amount, string countr, int numberOfMotor, string model1) : base(marka,year,volume,weight,amount,countr)
        {
            NamberOfMotor = numberOfMotor;
            model = model1;
        }

    } 
        class Program
    {
        static void Main(string[] args)
        {
            string messagePS = "/New Car Added";

            Mercedes Example = new Mercedes("Porshe", 2017, 4.5, 1900, 2, "Germany",12, "sedan");
            Example._Info
              += DisplayMessageAdvanced;
            Example._Info += delegate ()
            {
                Console.WriteLine("\n New car added to the data base.");
            };
            Example._Info += () => Console.WriteLine(messagePS);

            Example.Marka = "KIA";
            try
            {
                int x;
                Console.WriteLine("Enter car last year: ");
                x = Int32.Parse(Console.ReadLine());
                Example[Example.Length - 1] = x; ;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Wrong value entered");
                Console.ResetColor();
            }
            Car Nisan = new Car(3, "Japen");

            Nisan._Info += DisplayMessageAdvanced;

            Example._Info += delegate ()
            {
                Console.WriteLine("\nNew Car added to the data base.");
            };
            AgeComparer comparator = new AgeComparer();
            comparator.Compare(Example, Nisan);

            {
                Example._Info -= DisplayMessageAdvanced;

                Example._Info -= delegate ()
                {
                    Console.WriteLine("\n|CONSOLE MESSAGE| New Car added to the data base.");
                };

                Nisan._Info -= DisplayMessageAdvanced;

                Nisan._Info -= delegate ()
                {
                    Console.WriteLine("\n|CONSOLE MESSAGE| New student added to the data base.");
                };
            }


          }
        private static void DisplayMessageAdvanced()
        {
            Console.WriteLine("\n|CONSOLE MESSAGE| Data output is done.\n");
        }
    }
}
