using System;

namespace Titanic
{
    public class Passenger
    {
        public string Name;
        public string PClass;
        public double Age;
        public string Sex;
        public bool Survived;
        public bool Sexcode;

        public Passenger(string[] values)
        {
            Name = values[0];
            PClass = values[1];

            if (values[2] != "NA")
                Age = double.Parse(values[2]);
            else
                Age = -1;
            
            Sex = values[3];

            if (values[4] == "1")
                Survived = true;
            else
                Survived = false;
            
            if (values[5] == "1")
                Sexcode = true;
            else
                Sexcode = false;
        }
    }
}