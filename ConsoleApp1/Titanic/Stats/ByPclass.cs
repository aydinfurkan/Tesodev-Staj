using System;
using System.Collections.Generic;
using System.Linq;

namespace Titanic.Stats
{
    public class ByPclass : IStat
    {
        private List<int> SurviveByPClass;
        private List<int> DeathByPClass;
        public int Size { get; }
        
        public ByPclass()
        {
            SurviveByPClass = new List<int>();
            DeathByPClass = new List<int>();
            Size = 3;

            foreach (var currentPassenger in ReadPassengerList.AllPassengers)
            {
                if (currentPassenger.Survived)
                    SurviveByPClass.Add(currentPassenger.PClass[0] - 48);
                else
                    DeathByPClass.Add(currentPassenger.PClass[0] - 48);
            }
        }
        
        public List<string> Id()
        {
            return new List<string>{"1st", "2nd", "3rd"};
        }

        public List<int> SurvivedNumber()
        {
            return Methods.Group(SurviveByPClass, PClassIndex, Size);
        }

        public List<double> SurvivedRate()
        {
            var s = Methods.Group(SurviveByPClass, PClassIndex, Size);
            var d = Methods.Group(DeathByPClass, PClassIndex, Size);

            return Methods.Rate(s, d, Size);
        }
        
        private int PClassIndex(int pclass)
        {
            if (pclass == 1)
                return 0;
            if(pclass == 2)
                return 1;
            if (pclass == 3)
                return 2;
            
            return -1;
        }
    }
}