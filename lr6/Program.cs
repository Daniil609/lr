using System;
using System.Text;

namespace lab6
{
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

        public Student(int amount, string yniver) : this("Pasha", 18, "male", amount, yniver)
        {
            marks = new int[amount];
            Length = amount;
            VYS = yniver;
        }

        public Student() : this("Ivan", 18, "male", 3, "BSUIR")
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
            return ($"Name: {Name}\nAge: {age.ToString()}\nSex: {sex}\nVYS: {VYS}");
        }

        public override string ShowMarks()
        {
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

        // string Example.validity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Cadet(int auditoria, string validi) : this("Danik", 17, "male", 3, "MINDS", auditoria, validi)
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
        public static byte Input()
        {
            string number = Console.ReadLine();

            if (!byte.TryParse(number, out byte x))
            {
                Console.WriteLine("Incorrect input. Please enter number");
                return Input();
            }
            else
            {
                return x;
            }
        }

        public static Cadet[] CreateGroup()
        {
            Console.WriteLine("Enter amount of Cadets: ");

            int amount = Input();

            Cadet[] group = new Cadet[amount];

            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine($"Enter info about {(i + 1).ToString()} Cadets:(name, age, sex, amount of marks, VYS, auditoria, validity)");
                group[i] = new Cadet(Console.ReadLine(), Input(), Console.ReadLine(), Input(), Console.ReadLine(), Input(), Console.ReadLine());
                Console.Clear();
            }

            Console.Clear();

            return group;
        }

        public static void GetInfo(Cadet[] people)
        {
            Console.WriteLine("Info about all: \n");

            for (int i = 0; i < people.Length; i++)
            {
                Console.WriteLine($"Cadet {(i + 1).ToString()}: {people[i].Name}, {people[i].Age.ToString()}, {people[i].Sex}, {people[i].VYS}, {people[i].Auditoria.ToString()}, {people[i].Validity}\n");
            }
        }
        static void Main(string[] args)
        {

            while (true)
            {
                Cadet[] people = CreateGroup();

                GetInfo(people);

                Console.WriteLine("Continue?");

                if (Console.ReadLine() == "yes")
                {
                    Console.Clear();
                    continue;
                }
                else
                    break;
            }
        }


    }
}

