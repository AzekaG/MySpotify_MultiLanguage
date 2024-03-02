using Microsoft.AspNetCore.Mvc.Rendering;
using MySpotify.BLL.DTO;

namespace MySpotify.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<GenreDTO> genres, string genreName) 
        {
            genres.Insert(0, new GenreDTO() { Id = 0, Name = "all" });
            Genres = new SelectList(genres, "Id", "Name", genreName);
        }

        public SelectList Genres { get; } // cписок жанров

        public string Genre { get; }
        public string SelectedArtist { get; }

    }
}
