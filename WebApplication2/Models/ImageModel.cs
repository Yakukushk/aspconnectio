using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.Models
{
    public partial class ImageModel
    {
        [Key]
        public int id { get; set; }
       [Column(TypeName = "nvarchar(50)")]
        public string? Title { get; set; }
        [DisplayName("File Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string? Name { get; set; }
        
        
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? ImageFile { get; set; }
    }
}
