using MySpotify.BLL.DTO;

namespace MySpotify.Models.AdminViewModels
{
    public class AdminMediaEditModel
    {
        public MediaDTO? mediaDTO { get; set; }
        public IEnumerable<GenreDTO>? Genre { get; set; }

        public string? Poster { get; set; }

        public string? MediaFile {  get; set; }
      
    }
}
