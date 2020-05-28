using System;
using static System.Console;
using System.Collections.Generic;

namespace lr7
{
    class Comparer : IComparer<Ration>
    {
        public int Compare(Ration obj1, Ration obj2)
        {
            if (obj1.chisl * obj2.znam > obj2.chisl * obj1.znam)
            {
                return 1;
            }
            else if ((obj1.chisl == obj2.chisl) && (obj1.znam == obj2.znam))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
    struct Ration : IComparer<Ration>, IEquatable<Ration>
    {
        private int Chisl;
        private int Znam;

        public int chisl
        {
            get { return Chisl; }
        }

        public int znam
        {
            get { return Znam; }
        }

        public Ration (int _chisl) : this(_chisl, 1) { }

        public Ration (int _chisl, int _znam) 
        {
            if (_znam == 0) throw new ArgumentNullException();
            Chisl= _chisl;
            Znam = _znam;

        }

        //интерфейс
        public int CompareTO(Ration sravn)
        {
            return chisl.CompareTo(sravn);
        }

        public override bool Equals(object SravnNamber)
        {
            return Equals(SravnNamber);
        }
        public bool Equals(Ration SravnNamber)
        {
            Ration RatNamber = (Ration)SravnNamber;
            return (chisl == RatNamber.chisl &&
            znam == RatNamber.znam);
        }
        //перекрытие
        public static Ration operator +(Ration a,Ration b)
        {
            return new Ration(a.chisl * b.znam + b.chisl * a.znam,
                    a.znam * b.znam);
        }
        public static Ration operator -(Ration a,Ration b)
        {
            return new Ration(a.chisl * b.znam - b.chisl * a.znam,
                    a.znam * b.znam);
        }
        public static Ration operator *(Ration a,Ration b)
        {
            return new Ration(a.chisl*b.chisl,a.znam*b.znam);
        }
        public static Ration operator /(Ration a, Ration b)
        {
            return new Ration(a.chisl * b.znam,
                    a.znam * b.chisl);
        }
        public static bool operator <(Ration a, Ration b)
        {
            return a.chisl * b.znam
                < b.chisl * a.znam;
        }
        public static bool operator ==(Ration a, Ration b)
        {
            return a.Equals(b);
        }
        public static bool operator >(Ration a, Ration b)
        {
            return a.chisl * b.znam
                > b.chisl * a.znam;
        }
        public static bool operator !=(Ration a, Ration b)
        {
            return !a.Equals(b);
        }
        //разные форматы
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static explicit operator Ration(string number)
        {
            return Parse(number);
        }
        public static explicit operator string(Ration number)
        {
            return number.ToString();
        }
        public static implicit operator Ration(int number)
        {
            return new Ration(number);
        }

        public static explicit operator int(Ration number)
        {
            return Convert.ToInt32(number.chisl / number.znam);
        }
        public static explicit operator Ration(double doubleNum)
        {
            return new Ration(Convert.ToInt32(doubleNum * 100), 100);
        }

        public static explicit operator double(Ration number)
        {
            return number.chisl / number.znam;
        }
        //в строку
        public override string ToString()
        {
            return (chisl.ToString() + "/" + znam.ToString());
        }
        //из строки
        public static Ration Parse(string RatNumStr)
        {
            if (!string.IsNullOrEmpty(RatNumStr))
            {
                if (RatNumStr.IndexOf('/') != -1)
                {
                    string[] strnums = RatNumStr.Split('/');

                    for (int i = 0; i < strnums.Length; i++)
                    {
                        if (int.Parse(strnums[i]) <= 0 || string.IsNullOrEmpty(strnums[i]))
                        {
                            throw new FormatException("Invalid value parcing!");
                        }
                    }

                    return new Ration(int.Parse(strnums[0]),
                        int.Parse(strnums[1]));
                }
                else
                {
                    double thisIn;


                    int dop;
                    int thisOut;

                    thisIn = Convert.ToDouble(RatNumStr);

                    thisOut = Convert.ToInt32(Math.Floor(thisIn));
                    dop = Convert.ToInt32(Math.Round((thisIn - thisOut), 3) * 1000);

                    return new Ration((thisOut * 1000) + dop, 1000);

                }
            }
            else
            {
                throw new Exception("Invalid value parcing!");
            }

        }

        int IComparer<Ration>.Compare(Ration x, Ration y)
        {
            throw new NotImplementedException();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
        Ration[] RNumbers = null;
        MakeArray(ref RNumbers);
        Show(RNumbers);
        Comparing(RNumbers);

        }
        public static void MakeArray(ref Ration[] RNumbers)
        {
            string StrOfNums;
            WriteLine("How many numbers should create?");
            StrOfNums = ReadLine();
            int.TryParse((((string.IsNullOrEmpty(StrOfNums)) || (int.Parse(StrOfNums) <= 0)) ?
                "0" : StrOfNums), out int attempt);

            if (attempt <= 0)
            {
                Write("You are lying!");
                return;
            }

            int z = 1;
            RNumbers = new Ration[attempt];
            for (int i = 0; i < RNumbers.Length; i++)
            {
                string Str;
                int chisl, znam;

                while (true)
                {
                    WriteLine("Enter numerator #" + z.ToString());
                    Str = ReadLine();
                    if ((string.IsNullOrEmpty(Str)) || (int.Parse(Str) < 0))
                    {
                        WriteLine("You are lying, try again please!\n");
                    }
                    else
                    {
                        chisl = int.Parse(Str);
                        break;
                    }
                }

                while (true)
                {
                    WriteLine("Enter denumerator #" + z.ToString());
                    Str = ReadLine();
                    if ((string.IsNullOrEmpty(Str)) || (int.Parse(Str) < 0))
                    {
                        WriteLine("You are lying, try again please!\n");
                    }
                    else
                    {
                        znam = int.Parse(Str);
                        break;
                    }
                }
                z++;
                try
                {
                    RNumbers[i] = new Ration(chisl, znam);
                }
                catch (ArgumentNullException err)
                {
                    WriteLine(err.Message);
                }

                WriteLine("");
            }
        }
        public static void Show(Ration[] RNumbers)
        {
            for (int i = 0; i < RNumbers.Length; i++)
            {
                Write($"{i + 1}) ");
                WriteLine(RNumbers[i].chisl.ToString() + "/" + RNumbers[i].znam.ToString());
                WriteLine((RNumbers[i].chisl / RNumbers[i].znam).ToString());
                WriteLine("");
            }
        }
        public static void Comparing(Ration[] RNumbers)
        {
            string Str;
            int srav1 = 0, srav2 = 0;


            WriteLine("What numbers should I compare? Enter their counts in array");

            while (true)
            {
                WriteLine("Enter first number to compare");
                Str = ReadLine();
                if ((string.IsNullOrEmpty(Str))
                    || (int.Parse(Str) < 0)
                    || int.Parse(Str) > RNumbers.Length)
                {
                    WriteLine("You are lying, try again please!\n");
                }
                else
                {
                    srav1 = int.Parse(Str);
                }

                WriteLine("Enter second number to compare");
                Str = ReadLine();
                if ((string.IsNullOrEmpty(Str))
                    || (int.Parse(Str) < 0)
                    || int.Parse(Str) > RNumbers.Length)
                {
                    WriteLine("You are lying, try again please!\n");
                }
                else
                {
                    srav2 = int.Parse(Str);
                    break;
                }
            }

            Comparer objComp = new Comparer();
            if (objComp.Compare(RNumbers[srav1], RNumbers[srav2]) == 0)
            {
                WriteLine("These numbers are equal!");
            }
            else if (objComp.Compare(RNumbers[srav1], RNumbers[srav2]) == 1)
            {
                WriteLine("The first number is bigger than second one");
            }
            else
            {
                WriteLine("The second number is bigger than first one");
            }
        }
    }
}

