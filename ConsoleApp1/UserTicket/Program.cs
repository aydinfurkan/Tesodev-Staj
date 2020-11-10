using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> users;
            List<Ticket> tickets;
            
            var database = new Database();
            users = database.ReadUsers();
            tickets = database.ReadTickets();
            
            while (true)
            {
                Console.WriteLine("---------------");
                Console.WriteLine("1 - Login");
                Console.WriteLine("2 - Register");
                Console.WriteLine("3 - Database");
                Console.WriteLine("4 - Exit");
                Console.WriteLine("99 - Reset Database");
                Console.WriteLine(":");
                string f = Console.ReadLine();

                switch (f)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        Register();
                        break;
                    case "3":
                        database.Write();
                        break;
                    case "4":
                        return;
                    case "99":
                        database.Reset();
                        users = database.ReadUsers();
                        tickets = database.ReadTickets();
                        break;
                }
            }

            void Register()
            {
                Console.WriteLine("Username :");
                string username = Console.ReadLine();
                
                if (users.Exists(x => x.Username == username))
                {
                    Console.WriteLine("Username already taken!");
                    return;
                }
                
                Console.WriteLine("Password :");
                string password = Console.ReadLine();
                Console.WriteLine("Email :");
                string email = Console.ReadLine();
                
                var newUser = new User(Guid.NewGuid(),username, password, email, "User");
                users.Add(newUser);
                database.CreateUser(newUser);
            }
            
            void Login()
            {
                Console.WriteLine("Username :");
                string username = Console.ReadLine();
                Console.WriteLine("Password :");
                string password = Console.ReadLine();
                Console.WriteLine("---------------");

                if (users.Exists(x => x.Username == username && x.Password == password))
                {
                    Console.WriteLine("-------- Login Successful --------");
                    var currentUser = users.Find(x => x.Username == username);
                    if (currentUser.State == "User")
                    {
                        UserMenu(currentUser);
                    }
                    else
                    {
                        AdminMenu();
                    }
                    return;
                }
                
                Console.WriteLine("Login Fail");
                
            }

            void UserMenu(User currentUser)
            {
                Console.WriteLine("Email : {0}", currentUser.Email);
                Console.WriteLine("User level : {0}", currentUser.Description);
                while (true)
                {
                    Console.WriteLine(("---------------\n") +
                                      ("1 - Open Tickets\n") +
                                      ("2 - New Ticket\n") +
                                      ("3 - Logout\n") +
                                      (":"));
                    string f = Console.ReadLine();
                    Console.WriteLine("---------------");
                    switch (f)
                    {
                        case "1":
                            foreach (var ticket in tickets)
                            {
                                if (currentUser.TicketIds.Contains(ticket.Id))
                                {
                                    Console.WriteLine(" - " + ticket.Message);
                                    if (!ticket.IsOpen)
                                    {
                                        Console.WriteLine("     -> " + ticket.GetResponse());
                                    }
                                    else
                                    {
                                        Console.WriteLine("     -> " + "...");
                                    }
                                }

                            }

                            break;
                        case "2":
                            Console.WriteLine("New Ticket : ");
                            string message = Console.ReadLine();
                            var newTicket = new Ticket(message, currentUser.Username);
                            
                            tickets.Add(newTicket);
                            currentUser.AddTicketId(newTicket.Id);
                            
                            database.CreateTicket(newTicket);
                            database.UpdateUserTicketIds(currentUser);
                            break;
                        case "3":
                            return;
                    }
                }
            }

            void AdminMenu()
            {
                while (true)
                {
                    Console.WriteLine(("---------------\n") +
                                      ("1 - Solve Tickets\n") +
                                      ("2 - Logout\n") +
                                      (":"));
                    string f = Console.ReadLine();
                
                    switch (f)
                    {
                        case "1":
                            foreach (var currentTicket in tickets)
                            {
                                if (currentTicket.IsOpen)
                                {
                                    Console.WriteLine("---------------");
                                    Console.WriteLine(currentTicket.Username + " : ");
                                    Console.WriteLine(currentTicket.Message);
                                    Console.WriteLine("Response : ");
                                    string response = Console.ReadLine();
                                    currentTicket.SetResponse(response);
                                    database.UpdateResponse(currentTicket);
                                }
                            }
                            break;
                        case "2":
                            return;
                    } 
                }
            }

        }

    }
}