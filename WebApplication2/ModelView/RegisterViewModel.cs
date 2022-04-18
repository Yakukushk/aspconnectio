using System.ComponentModel.DataAnnotations;
namespace WebApplication2.ModelView
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
      

        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }

        //public string Name { get; set; }
        [Required]
        [Display(Name ="Password")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage ="Incorrect!")]
        [Display(Name ="PasswordConfirm")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
