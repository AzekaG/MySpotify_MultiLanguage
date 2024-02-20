using MySpotify.BLL.DTO;

namespace MySpotify.Models.AdminViewModels
{
    public class IndexMediaModel
    {
        public IEnumerable<GenreDTO> GenreList { get; set; }
        public IEnumerable<MediaDTO> MediaList { get; set; }


       

    }
}
