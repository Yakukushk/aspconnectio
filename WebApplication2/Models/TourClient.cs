using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class TourClient
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual Client Client { get; set; } = null!;
    }
}
