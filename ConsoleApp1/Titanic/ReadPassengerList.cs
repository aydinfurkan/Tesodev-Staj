using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Titanic
{
    public static class ReadPassengerList
    {
        public static List<Passenger> AllPassengers;
        
        public static void Read()
        {
            AllPassengers = new List<Passenger>();
            
            string filePath = "F:/RiderProjects/ConsoleApp1/Titanic/Titanic.csv";

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines.Skip(1))
            {
                var newPassenger = new string[6];
                var newLine = line.Split("\"");
                newPassenger[0] = newLine[3];
                newPassenger[1] = newLine[5];
                newPassenger[2] = newLine[6].Split(",")[1];
                newPassenger[3] = newLine[7];
                newPassenger[4] = newLine[8].Split(",")[1];
                newPassenger[5] = newLine[8].Split(",")[2];
                
                AllPassengers.Add(new Passenger(newPassenger));
            }
        }
    }
    
}