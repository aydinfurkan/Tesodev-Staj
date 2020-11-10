using System;
using System.Collections.Generic;

namespace Titanic.Stats
{
    public class BySex : IStat
    {
        private List<bool> SurviveBySex;
        private List<bool> DeathBySex;
        public int Size { get; }
        
        public BySex()
        {
            SurviveBySex = new List<bool>();
            DeathBySex = new List<bool>();
            Size = 2;
            foreach (var currentPassenger in ReadPassengerList.AllPassengers)
            {
                if (currentPassenger.Survived)
                    SurviveBySex.Add(currentPassenger.Sexcode);
                else
                    DeathBySex.Add(currentPassenger.Sexcode);
            }
        }
        
        public List<string> Id()
        {
            return new List<string> {"Male", "Female"};
        }

        public List<int> SurvivedNumber()
        {
            return Methods.Group(SurviveBySex, SexIndex, Size);
        }

        public List<double> SurvivedRate()
        {
            var s = Methods.Group(SurviveBySex, SexIndex, Size);
            var d = Methods.Group(DeathBySex, SexIndex, Size);

            return Methods.Rate(s, d, Size);
        }
        
        private int SexIndex(bool sex)
        {
            if (sex)
                return 1;
            else
                return 0;
        }
    }
}