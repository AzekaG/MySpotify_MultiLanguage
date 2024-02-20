using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpotify.BLL.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public string? Name { get; set; }

    }
}
