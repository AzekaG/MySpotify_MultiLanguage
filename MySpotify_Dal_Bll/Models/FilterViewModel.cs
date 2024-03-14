using Microsoft.AspNetCore.Mvc.Rendering;
using MySpotify.BLL.DTO;
using MySpotify.DAL.Entities;

namespace MySpotify.Models
{
    public class FilterViewModel
    {

        public SelectList Genres { get;}

        public int SelectGenre { get;}


        public FilterViewModel(List<GenreDTO> genres , int genre ) 
        {
            genres.Insert(0, new GenreDTO { Name = "All", Id = 0 });

            Genres = new SelectList(genres , "Id" , "Name" , genre);

            SelectGenre = genre;
        }


    }
}
