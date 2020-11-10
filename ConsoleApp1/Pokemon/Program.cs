using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pokemon
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new List<User>();
            ReadPokemon.Read();
            List<string> CombatLog = new List<string>();

            while (true)
            {
                Console.WriteLine("---------------");
                Console.WriteLine("1 - Login");
                Console.WriteLine("2 - Register");
                Console.WriteLine("3 - Exit");
                Console.WriteLine(":");
                string f = Console.ReadLine();
            
                switch (f)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        Register();
                        break;
                    case "3":
                        return;
                }
            }

            void Register()
            {
                Console.WriteLine("Username :");
                string username = Console.ReadLine();

                if (users.Exists(x => x.Username == username))
                {
                    Console.WriteLine("Username already taken!");
                    return;
                }
                
                Console.WriteLine("Password :");
                string password = Console.ReadLine();
                Console.WriteLine("Email :");
                string email = Console.ReadLine();
                
                Console.WriteLine("1 - Random 5 pokemon\n" +
                                  "2 - Choose 5 pokemon");
                string choose = Console.ReadLine();

                User newUser;
                switch (choose)
                {
                    case "2":
                        newUser = new User(username, password, email, false);
                        users.Add(newUser);
                        break;
                    default:
                        newUser = new User(username, password, email, true);
                        users.Add(newUser);
                        break;
                }

                Console.WriteLine("---------------");
                Console.WriteLine("Your Pokemon List : ");
                foreach (var currentPokemon in newUser.Deck.FivePokemon)
                {
                    Console.WriteLine(" - " + currentPokemon.Name);
                }

            }

            void Login()
            {
                Console.WriteLine("Username :");
                string username = Console.ReadLine();
                Console.WriteLine("Password :");
                string password = Console.ReadLine();
                Console.WriteLine("---------------");

                if (users.Exists(x => x.Username == username && x.GetPassword() == password))
                {
                    Console.WriteLine("-------- Login Successful --------");
                    Console.WriteLine("---------------");
                    Game(users.Find(x => x.Username == username));
                    return;
                }
                
                Console.WriteLine("Login Fail");
                
            }

            void Game(User player)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Hp" + "\t" + "Attack" + "\t" + "Defense"
                                      + "\t" + "Speed" + "\t" + "Legendary");
                    Console.WriteLine("-------------------------------");
                    
                    foreach (var playerPokemons in player.Deck.FivePokemon)
                    {
                        playerPokemons.Hp = playerPokemons.MaxHp;
                        Console.WriteLine(playerPokemons.Name + "\n" + playerPokemons.MaxHp 
                                          + "\t" + playerPokemons.Attack + "\t" + playerPokemons.Defense
                                          + "\t" + playerPokemons.Speed + "\t" + playerPokemons.Legendary);
                    }
                    
                    Console.WriteLine("1 - Find Battle");
                    Console.WriteLine("2 - Logout");
                    Console.WriteLine(":");
                    string f = Console.ReadLine();
                    
                    switch (f)
                    {
                        case "1":
                            int result = Battle(player.Deck, new Deck(true, "Enemy"));
                            if (result == -1)
                            {
                                Console.WriteLine(player.Username + " win the battle!");
                            }
                            else
                            {
                                Console.WriteLine("Enemy win the battle!");
                            }

                            Console.ReadLine();
                            break;
                        case "2":
                            return;
                    }
                }
                
            }

            int Battle(Deck player1, Deck player2)
            {
                Console.Clear();
                
                Console.WriteLine("{0,-25} " + " vs " + " {1,25}", player1.Username, player2.Username);
                Console.WriteLine("{0,-25} " + "    " + " {1,25}", "---------------", "---------------");
                for (int i = 0; i < player1.FivePokemon.Count; i++)
                {
                    Pokemon player1Pokemon = player1.FivePokemon[i];
                    Pokemon player2Pokemon = player2.FivePokemon[i];
                    Console.WriteLine("{0,-25} " + " vs " + " {1,25}", player1Pokemon.Name, player2Pokemon.Name);
                }

                Console.WriteLine("Press any key to start battle.");
                
                int score1 = 0;
                int score2 = 0;
                
                for (int i = 0; i < player1.FivePokemon.Count; i++)
                {
                    Console.ReadLine();
                    
                    if (score1 == 3 || score2 == 3)
                    {
                        return (score1 > score2) ? -1 : 1;
                    }
                    
                    int result = Fight(player1.FivePokemon[i], player2.FivePokemon[i]
                                        ,player1.Username, "Enemy", score1, score2);
                    if (result == -1)
                    {
                        score1 += 1;
                        Console.WriteLine("{0}.round - {1} ({2}) WIN!",i+1 , player1.FivePokemon[i].Name, player1.Username);
                    }
                    else
                    {
                        score2 += 1;
                        Console.WriteLine("{0}.round - {1} ({2}) WIN!",i+1 , player2.FivePokemon[i].Name, player2.Username);
                    }
                    
                }

                return 0;
            }

            int Fight(Pokemon pokemon1, Pokemon pokemon2, string player1Name, string player2Name, int score1, int score2)
            {
                
                bool turn = pokemon1.Speed > pokemon2.Speed;
                bool firstTurn = turn;
                CombatLog.Clear();
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("{0,-25} " + "{2}--{3}" + " {1,25}", player1Name, player2Name, score1, score2);
                    Console.WriteLine("{0,-25} " + " vs " + " {1,25}", pokemon1.Name, pokemon2.Name);
                    Console.WriteLine("{0,-25} " + "    " + " {1,25}", pokemon1.Hp + "/" + pokemon1.MaxHp, pokemon2.Hp + "/" + pokemon2.MaxHp);
                    Console.WriteLine("{0,-25} " + "    " + " {1,25}", pokemon1.HealthBar(), pokemon2.HealthBar());
                    
                    for (int i=0; i<CombatLog.Count; i++)
                    {
                        if(firstTurn && i%2 == 0 || !firstTurn && i%2 ==1)
                            Console.WriteLine("{0,54}", CombatLog[i]);
                        else
                            Console.WriteLine(CombatLog[i]);
                        
                    }
                    
                    if (pokemon2.Hp <= 0)
                    {
                        return -1;
                    }
                    if (pokemon1.Hp <= 0)
                    {
                        return 1;
                    }
                    
                    string s = Console.ReadLine();
                    
                    if (turn)
                    {
                        pokemon2 = Attack(pokemon1, pokemon2);
                    }
                    else
                    {
                        pokemon1 = Attack(pokemon2, pokemon1);
                    }

                    turn = !turn;

                }
                
            }

            Pokemon Attack(Pokemon attacker, Pokemon defender)
            {
                int attack = (attacker.Attack * 70 / 190) + 20;
                int defence = (defender.Defense * 60 / 230);

                int rand = new Random().Next(100);
                if (rand < 10)
                {
                    int damageRange = attack - defence;
                    int damage = new Random().Next(damageRange-5, damageRange+5);
                    damage =  damage > 0 ? damage : 0;
                    damage *= 2;
                    CombatLog.Add("-" + damage + " (Crit!)");
                    defender.Hp -= damage;
                }
                else if(rand >= 95)
                {
                    CombatLog.Add("(Miss!)");
                }
                else if(rand >= 80)
                {
                    int damageRange = attack - defence*2;
                    int damage = new Random().Next(damageRange-5, damageRange+5);
                    damage =  damage > 0 ? damage : 0;
                    CombatLog.Add("-" + damage + " (Block!)");
                    defender.Hp -= damage;
                }
                else
                {
                    int damageRange = attack - defence;
                    int damage = new Random().Next(damageRange-5, damageRange+5);
                    damage =  damage > 0 ? damage : 0;
                    CombatLog.Add("-" + damage);
                    defender.Hp -= damage;
                }

                return defender;
            }
            
        }

    }
}