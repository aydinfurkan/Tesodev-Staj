using System;
using System.Collections.Generic;

namespace Pokemon
{
    public class User
    {
        public string Username;
        private string Password;
        private string Email;
        public Deck Deck;

        public User(string username, string password, string email, bool isDeckRandom)
        {
            Username = username;
            Password = password;
            Email = email;
            Deck = new Deck(isDeckRandom, Username);
        }

        public string GetPassword()
        {
            return Password;
        }
        
        public string GetEmail()
        {
            return Email;
        }

    }
}