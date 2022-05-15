using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Excels
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public string Country { get; set; } = "";
        public int Price { get; set; } = 0;
        

    }
}
