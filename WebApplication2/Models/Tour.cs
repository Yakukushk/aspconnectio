using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public partial class Tour
    {
        [Key]
        public int Id { get; set; }
        
        
        public string? Name { get; set; }
        
        public int? Price { get; set; }
       
        public string? Info { get; set; }
       
        [DisplayName("File Name")]
        public string? Title { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? ImageFile { get; set; }
    }
}
