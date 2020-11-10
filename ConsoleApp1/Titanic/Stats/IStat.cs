using System.Collections.Generic;

namespace Titanic.Stats
{
    public interface IStat
    {
        public int Size { get; }
        public List<string> Id();
        public List<int> SurvivedNumber();
        public List<double> SurvivedRate();
    }
}