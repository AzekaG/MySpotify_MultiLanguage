using MySpotify.BLL.DTO;

namespace MySpotify.Models
{
    public class MediaUserGenreModel
    {
        public IEnumerable<MediaDTO> mediaDTOs { get; set; }
        public UserDTO userDTO { get; set; }
     
        public IEnumerable<MediaDTO> mediaUserDTOs {  get; set; }
    }
}
