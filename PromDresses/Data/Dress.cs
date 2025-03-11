using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace PromDresses.Data
{
    public class Dress
    {
        public int Id { get; set; } 
        public string CNumber {  get; set; }   
        public string NameDress { get; set; }
        public int CollectionId { get; set; }   
        public Collection Collections {  get; set; }
        public string Size {  get; set; }  
        public string Description {  get; set; }    
        public string URLimages {  get; set; }  
        public decimal Price {  get; set; } 
        public DateTime DateRegister { get; set; }  
    }
}
