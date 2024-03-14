using MySpotify.BLL.DTO;

namespace MySpotify.Models
{
    public class MediaUserGenreModel
    {
        public IEnumerable<MediaDTO> mediaDTOs { get; set; }
        public UserDTO? userDTO { get; set; }
        public MediaPaginationModel? mediaPaginationModel { get; set; }

        public SortViewModel sortViewModel { get; set; } = new SortViewModel(SortState.NameAsc);

        public FilterViewModel filterViewModel { get; set; }
    }



 
}
