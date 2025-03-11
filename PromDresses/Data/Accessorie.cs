using System.ComponentModel;

namespace PromDresses.Data
{
    public class Accessorie
    {
        public int Id { get; set; } 
        public string CNumber {  get; set; }   
        public string NameAccessorie {  get; set; } 
        public int CollectionId {  get; set; }    
        public Collection Collections { get; set; }
        public string Description {  get; set; }
        public string URLimages { get; set; }
        public decimal Price { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
