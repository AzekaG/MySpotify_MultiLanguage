using MySpotify.BLL.DTO;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace MySpotify.Models
{
    public class MediaDownloadModel
    {
        public IEnumerable<GenreDTO>? Genres { get; set; } 
        public MediaDTO? MediaDTO { get; set; }
       
		
    }
}
