namespace PromDresses.Data
{
    public class OrderDress
    {
        public int Id { get; set; }
        public string UserId {  get; set; } 
        public User Users { get; set; }
        public int DressId {  get; set; }
        public Dress Dress { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
