﻿namespace PromDresses.Data
{
    public class OrderAccessorie
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User Users { get; set; }
        public int AccessorieId {  get; set; }    
        public Accessorie Accessories { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
