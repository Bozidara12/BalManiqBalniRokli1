namespace PromDresses.Data
{
    public class Collection
    {
        public int Id {  get; set; }    
        public string Name { get; set; }    
        public DateTime DateRegister { get; set; }
        public ICollection<Dress>  Dresses { get; set; }
        public ICollection<Accessorie> Accessories { get; set; }

    }
}
