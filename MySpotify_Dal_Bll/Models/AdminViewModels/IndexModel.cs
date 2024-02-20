using MySpotify.BLL.DTO;
using MySpotify.BLL.Interfaces;

namespace MySpotify.Models.AdminViewModels
{
    public class IndexModel
    {
      public IEnumerable<GenreDTO> GenreList { get; set; }

    }
}
