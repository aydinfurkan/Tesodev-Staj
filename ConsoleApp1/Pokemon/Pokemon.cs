using System;

namespace Pokemon
{
    public class Pokemon
    {
        public string Name;
        public string Type1;
        public string Type2;
        public int Total;
        public double Hp;
        public double MaxHp;
        public int Attack;
        public int Defense;
        public int SpeedAtk;
        public int SpeedDef;
        public int Speed;
        public int Generation;
        public bool Legendary;

        public Pokemon(String[] values)
        {
            Name = values[1];
            Type1 = values[2];
            Type2 = values[3];
            Total = Int32.Parse(values[4]);
            MaxHp = Int32.Parse(values[5]);
            Hp = MaxHp;
            Attack = Int32.Parse(values[6]);
            Defense = Int32.Parse(values[7]);
            SpeedAtk = Int32.Parse(values[8]);
            SpeedDef = Int32.Parse(values[9]);
            Speed = Int32.Parse(values[10]);
            Generation = Int32.Parse(values[11]);
            Legendary = Boolean.Parse(values[12]);
        }

        public string HealthBar()
        {
            string bar = "";
            double maxBar = Math.Ceiling(MaxHp / 10);
            double currentBar = Math.Ceiling(Hp / 10);
            for (int i = 0; i < currentBar; i++)
            {
                bar += "H";
            }
            for (int i = 0; i < maxBar - currentBar; i++)
            {
                bar += "_";
            }

            return bar;
        }
    }
}