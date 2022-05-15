namespace WebApplication2.Models
{
    public class Info
    {
        public int id { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Categories { get; set; } = null!;
        public Tour Tour { get; internal set; }
    }
}
