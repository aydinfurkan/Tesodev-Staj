using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Titanic.Stats
{
    public class ByAge : IStat
    {
        private List<double> SurviveByAge;
        private List<double> DeathByAge;
        public int Size { get; }
        
        public ByAge()
        {
            SurviveByAge = new List<double>();
            DeathByAge = new List<double>();
            Size = 6;
            foreach (var currentPassenger in ReadPassengerList.AllPassengers)
            {
                if (currentPassenger.Survived)
                    SurviveByAge.Add(currentPassenger.Age);
                else
                    DeathByAge.Add(currentPassenger.Age);
            }
        }

        public List<string> Id()
        {
            return new List<string>{"NA", "0-20", "21-40" ,"41-60", "61-80", "81-100"};
        }
        
        public List<int> SurvivedNumber()
        {
            return Methods.Group(SurviveByAge, AgeIndex, Size);
        }

        public List<double> SurvivedRate()
        {
            var s = Methods.Group(SurviveByAge, AgeIndex, Size);
            var d = Methods.Group(DeathByAge, AgeIndex, Size);

            return Methods.Rate(s, d, Size);
        }

        private int AgeIndex(double age)
        {
            if (age == -1)
                return 0;
            if (age <= 20)
                return 1;
            if (20 < age && age <= 40)
                return 2;
            if (40 < age && age <= 60)
                return 3;
            if (60 < age && age <= 80)
                return 4;
            if (80 < age)
                return 5;
            
            return -1;
        }
    }
}