using Microsoft.AspNetCore.Identity;

namespace PromDresses.Data
{
    public class User:IdentityUser
    {
        public string FirstName {  get; set; }  
        public string LastName { get; set; }
        public string Address {  get; set; } 
        public ICollection<OrderDress> OrderDresses { get; set; }
        public ICollection<OrderAccessorie> OrderAccessories { get; set; }
    }
}
