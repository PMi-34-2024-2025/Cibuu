﻿namespace Cibuu.DAL.models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string[] FavoritePlaces { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } 
    }
}
