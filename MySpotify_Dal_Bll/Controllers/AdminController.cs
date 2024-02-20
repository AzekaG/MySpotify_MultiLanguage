using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySpotify.BLL.DTO;
using MySpotify.BLL.Interfaces;
using MySpotify.DAL.Entities;
using MySpotify.Filters;
using MySpotify.Models.AdminViewModels;

namespace MySpotify.Controllers
{
	[Culture]
	public class AdminController : Controller
    {
        
        readonly IGenreService _genreService;
        
        public AdminController(IGenreService genreService)
        {
            
            _genreService = genreService;
            
        }

        // GET: AdminController
        public async Task<ActionResult> Index()
        {
            HttpContext.Session.SetString("path", Request.Path);
            IndexModel indexModel = new IndexModel()
            {
                GenreList = await _genreService.GetGenres()
            };
            return View(indexModel);
        }

        // GET: AdminController/Edit/5
        public async Task<IActionResult> EditGenre(int? id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (id == null)
            {
                return NotFound();
            }
           
            var genre = await _genreService.Get((int)id);
            if (genre == null)
            {
                return NotFound();
            }
            return PartialView("EditGenre", genre);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditGenre(int id, GenreDTO genre)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (id != genre.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                   await _genreService.UpdateGenre(genre);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _genreService.GenreExist(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DeleteGenre(int? id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (id == null)
            {
                return NotFound();
            }

            var genre =await  _genreService.Get((int)id);
            if (genre == null)
            {
                return NotFound();
            }
            return PartialView("DeleteGenre", genre);
        }
        //removeJenre
        [HttpPost]
        public async Task<IActionResult> DeleteGenre(int id, GenreDTO genre)
        {

            HttpContext.Session.SetString("path", Request.Path);
            if (id != genre.Id)
            {
                return NotFound();
            }
            if (id>0)
            {
                try
                {
                   await _genreService.DeleteGenre(id);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _genreService.GenreExist(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return RedirectToAction("Index");
        }

        public IActionResult CreateGenre()
        {
            return PartialView("CreateGenre");
        }

        //createGenre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenre(GenreDTO genre)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (ModelState.IsValid)
            {
                await _genreService.CreateGenre(genre);
                return Ok();
            }
            return PartialView("CreateGenre");
        }



    }
}
