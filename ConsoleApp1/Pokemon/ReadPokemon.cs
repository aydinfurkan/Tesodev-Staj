using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pokemon
{
    public static class ReadPokemon
    {
        public static List<Pokemon> AllPokemons;
        
        public static void Read()
        {
            AllPokemons = new List<Pokemon>();
            
            string filePath = "F:/RiderProjects/ConsoleApp1/Pokemon/Pokemon.csv";

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines.Skip(1))
            {
                AllPokemons.Add(new Pokemon(line.Split(",")));
            }
        }
    }
}