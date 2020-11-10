using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string[]> data = new List<string[]>();
            
            while (true)
            {
                Console.WriteLine("---------------");
                Console.WriteLine("1 - Login");
                Console.WriteLine("2 - Register");
                Console.WriteLine(":");
                int f = Int32.Parse(Console.ReadLine());

                switch (f)
                {
                    case 1:
                        
                        Console.WriteLine("Kullanici adi :");
                        string username = Console.ReadLine();
                        Console.WriteLine("Sifre :");
                        string password = Console.ReadLine();
                        Console.WriteLine("---------------");

                        bool isExist = false;
                        foreach (var list in data)
                        {
                            if (list[0] == username && list[1] == password)
                            {
                                isExist = true;
                                foreach (var a in list)
                                {
                                    Console.WriteLine(a);
                                }
                            }
                        }

                        if (!isExist)
                        {
                            Console.WriteLine("Kullanici bulunamadi!");
                        }
                        
                        break;
                    case 2:
                        
                        Console.WriteLine("Kullanici adi :");
                        string user = Console.ReadLine();
                        Console.WriteLine("Sifre :");
                        string pass = Console.ReadLine();
                        Console.WriteLine("Email :");
                        string email = Console.ReadLine();
                        Console.WriteLine("Dogum tarihi :");
                        string birth = Console.ReadLine();

                        string[] info = new[] {user, pass, email, birth};
                        data.Add(info);
                        
                        break;
                }
            }
        }
    }
}