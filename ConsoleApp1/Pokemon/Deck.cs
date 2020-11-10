using System;
using System.Collections.Generic;

namespace Pokemon
{
    public class Deck
    {
        public string Username;
        public List<Pokemon> FivePokemon;
        public Deck(bool isDeckRandom, string username)
        {
            FivePokemon = new List<Pokemon>();
            Username = username;
            
            if (isDeckRandom)
            {
                for (int i = 0; i < 5; i++)
                {
                    Pokemon randomPokemon = ReadPokemon.AllPokemons[new Random().Next(801)];
                    FivePokemon.Add(randomPokemon);
                }
            }
            else
            {
                while (FivePokemon.Count < 5)
                {
                    Console.WriteLine("Pokemon name : ");
                    string PokemonName = Console.ReadLine();

                    var PokemonList = FindPokemon(PokemonName);

                    if (PokemonList.Count == 1)
                    {
                        Console.WriteLine(PokemonList[0].Name + " Added.");
                        FivePokemon.Add(PokemonList[0]);
                    }
                    else if (PokemonList.Count > 0)
                    {
                        for (int i = 0; i < PokemonList.Count; i++)
                        {
                            Console.WriteLine(i + " - " + PokemonList[i].Name);
                        }
                        Console.WriteLine(":");
                            
                        int c = Int32.Parse(Console.ReadLine());
                            
                        Console.WriteLine(PokemonList[c].Name + " Added.");
                        FivePokemon.Add(PokemonList[c]);
                            
                    }
                    else
                    {
                        Console.WriteLine("Pokemon not found!");
                    }
                }
            }
        }
        private List<Pokemon> FindPokemon(string PokemonName)
        {
            var PokemonList = new List<Pokemon>();
                
            foreach (var CurrentPokemon in ReadPokemon.AllPokemons)
            {
                string CurrentName = CurrentPokemon.Name;
                bool flag = false;
                for (int i = 0; i < Math.Min(CurrentName.Length, PokemonName.Length); i++)
                {
                    if (PokemonName[i] != CurrentName[i])
                    {
                        flag = true;
                        break;
                    }
                }
                    
                if (!flag)
                {
                    PokemonList.Add(CurrentPokemon);
                }
            }
            return PokemonList;
        }

        
    }
}