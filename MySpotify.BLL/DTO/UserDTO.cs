using MySpotify.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpotify.BLL.DTO
{
    public enum Status
    {
        admin,
        user
    }
    public class UserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]
      
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]

        public string? LastName { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public Status? Status { get; set; } 

       
    }
}
