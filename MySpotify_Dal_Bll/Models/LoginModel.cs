using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MySpotify.Resourses;

namespace MySpotify.Models
{
    public class LoginModel
    {

		    [Required(ErrorMessageResourceType = typeof(Resourses.Resource), ErrorMessageResourceName = "Login_Requered")]
		    [Display(Name = "Login" , ResourceType = typeof(Resourses.Resource))]
            public string? Email { get; set; }
            [Required(ErrorMessageResourceType = typeof(Resourses.Resource), ErrorMessageResourceName = "Pass_Requered")]
            [DisplayName("Пароль")]
            [DataType(DataType.Password)]
            public string? Password { get; set; }

    }



}

