using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ConsoleApp3
{
    public class Database
    {
        private SQLiteConnection Connection;
        private string DateTimeFormat = "dd-MM-yyyy HH:mm:ss";
        private string DbPath = "F:/RiderProjects/ConsoleApp1/UserTicket.db";

        public Database()
        {
            
            if (!File.Exists(DbPath))
            {
                SQLiteConnection.CreateFile(DbPath);
                Connection = new SQLiteConnection("Data Source="+DbPath+";Version=3;");
                CreateTables();
            }
            else
            {
                Connection = new SQLiteConnection("Data Source="+DbPath+";Version=3;");
            }
            
        }
        
        private void CreateTables()
        {
            Connection.Open();
            
            SQLiteCommand cmd = new SQLiteCommand(Connection);
            
            cmd.CommandText = @"CREATE TABLE users(id TEXT, 
                                username TEXT, 
                                password TEXT, 
                                email TEXT, 
                                state TEXT, 
                                ticketIds TEXT,
                                createdTime TEXT,
                                updatedTime TEXT)";
            
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = @"CREATE TABLE tickets(id TEXT, 
                                username TEXT, 
                                message TEXT, 
                                response TEXT,
                                isOpen INT, 
                                createdTime TEXT,
                                updatedTime TEXT)";
            
            cmd.ExecuteNonQuery();
            
            Connection.Close();
            
            CreateUser(new User(Guid.NewGuid(), "admin", "1234", "admin@1234.com", "Admin"));

        }
        public void CreateUser(User user)
        {
            Connection.Open();
            
            SQLiteCommand cmd = new SQLiteCommand(Connection);
            cmd.CommandText = "INSERT INTO users(id, username, password, email, state, ticketIds, createdTime, updatedTime) " + 
                              "VALUES($id, $username, $password, $email, $state, '', $createdTime, $updatedTime)";
            cmd.Parameters.Add("$id", DbType.String).Value = user.Id;
            cmd.Parameters.Add("$username", DbType.String).Value = user.Username;
            cmd.Parameters.Add("$password", DbType.String).Value = user.Password;
            cmd.Parameters.Add("$email", DbType.String).Value = user.Email;
            cmd.Parameters.Add("$state", DbType.String).Value = user.State;
            cmd.Parameters.Add("$createdTime", DbType.String).Value = user.CreatedTime.ToString(DateTimeFormat);
            cmd.Parameters.Add("$updatedTime", DbType.String).Value = user.UpdatedTime.ToString(DateTimeFormat);
            cmd.ExecuteNonQuery();
            
            Connection.Close();
        }
        public void CreateTicket(Ticket ticket)
        {
            Connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(Connection);

            cmd.CommandText = "INSERT INTO tickets(id, username, message, response, isOpen, createdTime, updatedTime) " + 
                              "VALUES($id, $username, $message, $response, $isOpen, $createdTime, $updatedTime)";
            cmd.Parameters.Add("$id", DbType.String).Value = ticket.Id;
            cmd.Parameters.Add("$username", DbType.String).Value = ticket.Username;
            cmd.Parameters.Add("$message", DbType.String).Value = ticket.Message;
            cmd.Parameters.Add("$response", DbType.String).Value = ticket.GetResponse();
            cmd.Parameters.Add("$isOpen", DbType.Int32).Value = ticket.IsOpen;
            cmd.Parameters.Add("$createdTime", DbType.String).Value = ticket.CreatedTime.ToString(DateTimeFormat);
            cmd.Parameters.Add("$updatedTime", DbType.String).Value = ticket.UpdatedTime.ToString(DateTimeFormat);
            cmd.ExecuteNonQuery();
            
            Connection.Close();
        }
        public void UpdateResponse(Ticket ticket)
        {
            Connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(Connection);

            cmd.CommandText = "UPDATE tickets set response = $response, updatedTime = $updatedTime, isOpen = $isOpen where id = $id";
            cmd.Parameters.Add("$id", DbType.String).Value = ticket.Id;
            cmd.Parameters.Add("$response", DbType.String).Value = ticket.GetResponse();
            cmd.Parameters.Add("$updatedTime", DbType.String).Value = ticket.UpdatedTime.ToString(DateTimeFormat);
            cmd.Parameters.Add("$isOpen", DbType.Int32).Value = ticket.IsOpen;
            cmd.ExecuteNonQuery();

            Connection.Close();
        }
        public void UpdateUserTicketIds(User user)
        {
            Connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(Connection);
            
            var ticketIds = JsonSerializer.Serialize(user.TicketIds);
            
            cmd.CommandText = "UPDATE users set ticketIds = $ticketIds, updatedTime = $updatedTime where id = $id";
            cmd.Parameters.Add("$id", DbType.String).Value = user.Id;
            cmd.Parameters.Add("$ticketIds", DbType.String).Value = ticketIds;
            cmd.Parameters.Add("$updatedTime", DbType.String).Value = user.UpdatedTime.ToString(DateTimeFormat);
            cmd.ExecuteNonQuery();
            
            Connection.Close();
        }
        public void Write()
        {
            Connection.Open();
            
            string stm = "SELECT * FROM users";
            SQLiteCommand cmd = new SQLiteCommand(stm, Connection);
            
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine("-----Users-----");
            while (rdr.Read())
            {
                Console.WriteLine($"{rdr.GetString(0)} / {rdr.GetString(1),-15} / " +
                                  $"{rdr.GetString(2),-10} / {rdr.GetString(3),-20} / {rdr.GetString(4)} / " +
                                  $"{rdr.GetString(6)} / {rdr.GetString(7)} / {rdr.GetString(5)}");
            }

            string stm2 = "SELECT * FROM tickets";
            SQLiteCommand cmd2 = new SQLiteCommand(stm2, Connection);
            
            using SQLiteDataReader rdr2 = cmd2.ExecuteReader();
            Console.WriteLine("-----Tickets-----");
            while (rdr2.Read())
            {
                Console.WriteLine($"{rdr2.GetString(0)} / {rdr2.GetString(1),-15} / " +
                                  $"{rdr2.GetString(2),-20} / {rdr2.GetString(3),-20} / {rdr2.GetInt32(4)} / " +
                                  $"{rdr2.GetString(5)} / {rdr2.GetString(6)}");
            }
            
            Connection.Close();
        }
        public List<User> ReadUsers()
        {
            Connection.Open();
            
            string stm = "SELECT * FROM users";
            SQLiteCommand cmd = new SQLiteCommand(stm, Connection);
            
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            
            var users = new List<User>();
            while (rdr.Read())
            {
                var ticketIds = JsonConvert.DeserializeObject<List<Guid>>(rdr.GetString(5));
                DateTime createdTime = DateTime.ParseExact(rdr.GetString(6), DateTimeFormat, CultureInfo.InvariantCulture);
                DateTime updatedTime = DateTime.ParseExact(rdr.GetString(7), DateTimeFormat, CultureInfo.InvariantCulture);
                
                var user = new User(rdr.GetGuid(0),rdr.GetString(1), rdr.GetString(2),
                                rdr.GetString(3), rdr.GetString(4), ticketIds,
                                createdTime, updatedTime);
                
                users.Add(user);
            }

            Connection.Close();
            return users;
        }
        public List<Ticket> ReadTickets()
        {
            Connection.Open();
            
            string stm = "SELECT * FROM tickets";
            SQLiteCommand cmd = new SQLiteCommand(stm, Connection);
            
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            
            var tickets = new List<Ticket>();
            while (rdr.Read())
            {
                bool isOpen = rdr.GetInt32(4) != 0;
                DateTime createdTime = DateTime.ParseExact(rdr.GetString(5), DateTimeFormat, CultureInfo.InvariantCulture);
                DateTime updatedTime = DateTime.ParseExact(rdr.GetString(6), DateTimeFormat, CultureInfo.InvariantCulture);
                
                var ticket = new Ticket(rdr.GetGuid(0), rdr.GetString(1),
                    rdr.GetString(2), rdr.GetString(3), isOpen,
                    createdTime, updatedTime);
                
                tickets.Add(ticket);
            }

            Connection.Close();
            return tickets;
        }
        public void Reset()
        {
            Connection.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            Console.Clear();
            File.Delete(DbPath);
            SQLiteConnection.CreateFile(DbPath);
            Connection = new SQLiteConnection("Data Source="+DbPath+";Version=3;");
            CreateTables();
        }
    }
}