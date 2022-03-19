using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Payment { get; set; } = null!;
        public string Location { get; set; } = null!;

        public virtual TourClient IdNavigation { get; set; } = null!;
    }
}
