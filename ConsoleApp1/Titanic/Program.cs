using System;
using Titanic.Stats;

namespace Titanic
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadPassengerList.Read();

            while (true)
            {
                IStat stat;
                
                Console.WriteLine("---------------");
                Console.WriteLine("1 - By Age");
                Console.WriteLine("2 - By Sex");
                Console.WriteLine("3 - By PClass");
                Console.WriteLine("4 - Custom");
                Console.WriteLine("5 - Exit");
                Console.WriteLine(":");

                string input = Console.ReadLine();
                
                switch (input)
                {
                    case "1":
                        stat = new ByAge();
                        Show(stat);
                        break;
                    case "2":
                        stat = new BySex();
                        Show(stat);
                        break;
                    case "3":
                        stat = new ByPclass();
                        Show(stat);
                        break;
                    case "4":
                        Guess();
                        break;
                    case "5":
                        return;
                }
                
            }

            void Show(IStat stat)
            {
                Console.WriteLine("---------------");
                for (int i = 0; i < stat.Size; i++)
                {
                    Console.WriteLine(stat.Id()[i] + ":" + stat.SurvivedNumber()[i] + " (%{0:0.00})", stat.SurvivedRate()[i]*100);
                }
                
            }

            void Guess()
            {
                Console.Clear();
                Console.WriteLine("Sex (0:Male, 1:Female) : ");
                int sex = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Age (0:NA, 1:0-20, 2:21-40 ,3:41-60, 4:61-80, 5:81-100) : ");
                int age = Int32.Parse(Console.ReadLine());
                Console.WriteLine("PClass (0:1st, 1:2nd, 2:3rd) : ");
                int pClass = Int32.Parse(Console.ReadLine());
                
                var ageRate = new ByAge().SurvivedRate()[age];
                var sexRate = new BySex().SurvivedRate()[sex];
                var pClassRate = new ByPclass().SurvivedRate()[pClass];

                var surviveRate = ageRate * sexRate * pClassRate;
                
                if(new Random().Next(10000) <= surviveRate*10000)
                    Console.WriteLine("Survived" + " (%{0:0.00})", surviveRate*100);
                else
                    Console.WriteLine("Died" + " (%{0:0.00})", surviveRate*100);

            }
            
        }
    }
}