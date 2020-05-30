using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;

namespace lab6
{
    class AllPeople
    {
        Person[] data;
        public AllPeople(int kolvo)
        {
            data = new Person[kolvo];
        }
        public Person this[int index]
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
    public interface IComparer<T>
    {
        int Compare(T up1, T up2);
    }
    class AgeComparer : IComparer<Person>
    {
        public int Compare(Person ps1, Person ps2)
        {
            if (ps1.Age > ps2.Age)
            {
                Console.WriteLine($"\nPerson {ps1.Name} is older, than {ps2.Name}.");
                return 1;
            }
            else
            {
                Console.WriteLine($"\nPerson {ps1.Name} is younger, than {ps2.Name}.");
                return 0;
            }
        }
    }
    interface NewData<T>
    {
        T Weight { get; }
        T Height { get; }
    }
    interface ThisPerson : NewData<int>
    {
        string Name { get; }
        string Surname { get; }

    }
    abstract class Person : ThisPerson
    {
        protected byte age;

        protected string sex;

        public string Name { get; set; }

        protected int ID;

        public int Height { get; set; }

        public int Weight { get; set; }

        public string Surname { get; set; }

        public delegate void Information();

        public Person() : this("Unknown", 0, "Unknown", CreateID())
        { }

        public Person(string name) : this(name, 0, "Unknown", CreateID())
        { }

        public Person(string name, byte age) : this(name, age, "Unknown", CreateID())
        { }

        public Person(string name, byte age, string sex, int id)
        {
            Name = name;
            Age = age;
            Sex = sex;
            ID = id;
        }

        public Person(string name, byte age, string sex) : this(name, age)
        {
            this.sex = sex;
        }

        public string Sex
        {
            set
            {

                if (value == "male" || value == "female")
                    sex = value;
                else
                    sex = "Unknown";
            }

            get { return sex; }
        }

        public int CompareTo(object obj)
        {
            Person m = obj as Person;
            if (m != null)
                return ID.CompareTo(m.ID);
            throw new Exception("Невозможно сравнить эти объекты!");
        }

        public byte Age
        {
            set
            {
                if (value >= 1 && value <= 100)
                    age = value;
                else
                    age = 0;
            }

            get { return age; }
        }

        public virtual string Show()
        {
            return ($"Name: {Name}\nAge: {age.ToString()}\nSex: {sex}");
        }

        static Random rand = new Random();

        public static int CreateID()
        {
            return rand.Next(1000, 10000);
        }

        public abstract string ShowMarks();
    }
    

   

    class Student : Person
    {
        protected int[] marks;
        protected string Vys;
        public int Length { get; private set; }
        public enum Course
        {
            First = 1,
            Second,
            Third,
            Fourth,
            Fifth,
            Magistrant
        }

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
        public string VYS
        {
            get
            {
                if (Vys[0] == 'B') return Vys;
                else return Vys + " isn't belorussian";
            }
            set
            {
                if (value[0] == 'B') Vys = value;
                else Vys = 'B' + value;
            }
        }

        public Course CURS { get; set; }

        public Student(string name, byte age, string sex, int amount, string yniver) : base(name, age, sex)
        {
            marks = new int[amount];
            Length = amount;
            VYS = yniver;
        }

        public Student(string name, int amount) : this(name, 18, "Unknown", amount, "BSUIR")
        {
            marks = new int[amount];
            Length = amount;
        }

        public Student(int amount, string yniver) : this("Alex", 18, "male", amount, yniver)
        {
            marks = new int[amount];
            Length = amount;
            VYS = yniver;
        }

        public Student() : this("Alex", 18, "male", 3, "BSUIR")
        { }


        public int this[int index]
        {
            get
            {
                if (index > 0 && index <= Length)
                    return marks[index - 1];
                else
                    return marks[0];
            }
            set
            {
                if (index > 0 && index <= Length && value <= 10 && value >= 0)
                    marks[index - 1] = value;
                else
                    throw new ArgumentException();
                //Console.WriteLine("incorrect input");
            }
        }
        public new virtual string Show()
        {
            Info?.Invoke();
            return ($"Name: {Name}\nAge: {age.ToString()}\nSex: {sex}\nVYS: {VYS}");
        }
    

        public override string ShowMarks()
        {
            Info?.Invoke();
            string ocenki = "";
            foreach (int mark in marks)
                ocenki += mark.ToString() + " ";
            return ocenki;
        }
    }
    public interface IExample
    {
        public int Auditoria { get; set; }
        public string Validity { get; set; }

    }
    sealed class Cadet : Student, IExample
    {
        public int Auditoria { get; set; }
        private string validity;

        public string Validity
        {
            get
            {
                return validity;
            }
            set
            {
                if (value == "fit" && value == "unfit")
                    validity = value;
                else
                {
                    validity = "Unknown " + value;
                    throw new ArgumentException();
                }

            }
        }

        

        public Cadet(int auditoria, string validi) : this("Ivan", 17, "male", 3, "MINDS", auditoria, validi)
        {
            Auditoria = auditoria;
            validity = validi;

        }

        public Cadet(string name, byte age, string sex, int amount, string yniver, int aoditoria, string validi) : base(name, age, sex, amount, yniver)
        {
            Auditoria = aoditoria;
            validity = validi;

        }


    }




    class Program
    {
        static void Main(string[] args)
        {
            string messagePS = "/New Cadet Added";

            Cadet Example = new Cadet("Ivan", 17, "male", 10, "MINDA", 55, "fit");
            Example._Info
                += DisplayMessageAdvanced;
            Example._Info += delegate ()
            {
                Console.WriteLine("\n New cadet added to the data base.");
            };

            Example._Info += () => Console.WriteLine(messagePS);

            Example.Surname = "Ivanov";
            Example.CURS = Student.Course.First;

            try
            {
                int x;
                Console.WriteLine("Enter Ivan last mark: ");
                x = Int32.Parse(Console.ReadLine());
                Example[Example.Length - 1] = x; ;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Wrong value entered");
                Console.ResetColor();
            }

            Student Alex = new Student(10, "BSUIR");

            Alex._Info += DisplayMessageAdvanced;

            Example._Info += delegate ()
            {
                Console.WriteLine("\nNew student added to the data base.");
            };

            Alex.Surname = "Petrov";
            

            try
            {
                int x;
                Console.WriteLine("Enter Alex height: ");
                x = Int32.Parse(Console.ReadLine());
                Alex.Height = x;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Wrong value entered");
                Console.ResetColor();
            }


                AgeComparer comparator = new AgeComparer();
                comparator.Compare(Example, Alex);

            {
                Example._Info -= DisplayMessageAdvanced;

                Example._Info -= delegate ()
                {
                    Console.WriteLine("\n|CONSOLE MESSAGE| New Cadet added to the data base.");
                };

                Alex._Info -= DisplayMessageAdvanced;

                Alex._Info -= delegate ()
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

