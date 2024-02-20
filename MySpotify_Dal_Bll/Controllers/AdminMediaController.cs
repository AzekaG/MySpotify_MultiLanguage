using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySpotify.BLL.DTO;
using MySpotify.BLL.Interfaces;
using MySpotify.Filters;
using MySpotify.Models;
using MySpotify.Models.AdminViewModels;

namespace MySpotify.Controllers
{
	[Culture]
	public class AdminMediaController : Controller
    {

        readonly IMediaService _mediaService;
        readonly IGenreService _genreService;

        public AdminMediaController(IMediaService mediaService, IGenreService genreService)
        {

            _mediaService = mediaService;
            _genreService = genreService;
        }

        // GET: AdminController
        public async Task<ActionResult> Index()
        {
            HttpContext.Session.SetString("path", Request.Path);
            IndexMediaModel indexModel = new IndexMediaModel()
            {
                MediaList = await _mediaService.GetMediaList(),
                GenreList = await _genreService.GetGenres()
            };
            return View(indexModel);
        }

        // GET: AdminController/Edit/5
        public async Task<IActionResult> EditMedia(int? id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (id == null)
            {
                return NotFound();
            }

            var media = await _mediaService.GetMedia((int)id);
            if (media == null)
            {
                return NotFound();
            }

            AdminMediaEditModel adminMediaEditModel = new AdminMediaEditModel() { mediaDTO = media , Genre = await _genreService.GetGenres()};


            return PartialView("EditMedia", adminMediaEditModel);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<ActionResult> EditMedia(AdminMediaEditModel media , string genreId)
         {
            HttpContext.Session.SetString("path", Request.Path);
            media.mediaDTO.Genre = genreId;
           
            if (ModelState.IsValid)
            {
                try
                {
                    await _mediaService.UpdateMedia(media.mediaDTO);

                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();

                }
                return Ok();
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DeleteMedia(int? id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (id == null)
            {
                return NotFound();
            }

            var media = await _mediaService.GetMedia((int)id);
            if (media == null)
            {
                return NotFound();
            }
            return PartialView("DeleteMedia", media);
        }
        //removeJenre
        [HttpPost]
        public async Task<IActionResult> DeleteMedia(int id, MediaDTO media)
        {

            HttpContext.Session.SetString("path", Request.Path);
            if (id != media.Id)
            {
                return NotFound();
            }
            if (id > 0)
            {
                try
                {
                    await _mediaService.DeleteMedia(id);

                }
                catch (DbUpdateConcurrencyException)
                {
                     return NotFound();
                  
                }
                return Ok();
            }
            return RedirectToAction("Index");
        }

     
    }
}
