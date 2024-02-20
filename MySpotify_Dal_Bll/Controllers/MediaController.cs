using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySpotify.BLL.DTO;
using MySpotify.BLL.Interfaces;
using MySpotify.DAL.Entities;
using MySpotify.Filters;
using MySpotify.Models;

namespace MySpotify.Controllers
{
	[Culture]
	public class MediaController : Controller
    {
        readonly IUserService _userService;
        readonly IGenreService _genreService;
        readonly IMediaService _mediaService;
        readonly IWebHostEnvironment _environment;
        public MediaController(IUserService userService, IGenreService genreService, IMediaService mediaService , IWebHostEnvironment environment)
        {
            _userService = userService;
            _genreService = genreService;
            _mediaService = mediaService;
            _environment = environment;
        }


        // GET: MediaControlelr
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("path", Request.Path);
            if(!isLogged())
            {
                return View("IndexUnlogged", new MediaUserGenreModel
                {
                    mediaDTOs = await _mediaService.GetMediaList(),
                    userDTO = null
                });
            }
            var user = await _userService.GetUser(int.Parse(HttpContext.Session.GetString("Id")));
            return View("IndexLogged", new MediaUserGenreModel
            {
                mediaDTOs = await _mediaService.GetMediaList(),
                userDTO = user,
                mediaUserDTOs = (await _mediaService.GetMediaList()).Where(x=>x.UserId == user.Id )
            });
        }




        // GET: MediaControlelr/Details/5
        public ActionResult Details(int id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        }

        // GET: MediaControlelr/Create
        public async Task<ActionResult> Create()
        {
			HttpContext.Session.SetString("path", Request.Path);
			MediaDownloadModel mediaDownloadModel = new MediaDownloadModel() 
            {
                Genres = await _genreService.GetGenres(),
               
            };
			return View(mediaDownloadModel);
        }

        // POST: MediaControlelr/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MediaDownloadModel media, IFormFile? upPoster, IFormFile? upMedia , string genre)
        {
			HttpContext.Session.SetString("path", Request.Path);
			media.MediaDTO.Genre = (await _genreService.Get(int.Parse(genre))).Name;
            
            if (!isLogged())
            {
                return RedirectToAction("Index", "Media");
            }

            string MediaAdress = String.Empty;
            string PosterAdress = String.Empty;

            if (upPoster != null && upMedia != null)
            {
                MediaAdress = "/Media/Music/" + upMedia.FileName;
                PosterAdress = "/Media/Poster/" + upPoster.FileName;
            }
            media.MediaDTO.FileAdress = MediaAdress;
            media.MediaDTO.Poster = PosterAdress;
            media.MediaDTO.UserId = int.Parse(HttpContext.Session.GetString("Id"));
            media.MediaDTO.rootPath = _environment.WebRootPath;
			 
            if (ModelState.IsValid)
            {
                await _mediaService.CreateMedia(media.MediaDTO , upPoster , upMedia);
                return RedirectToAction("Index", "Media");
            }
            return View(media);
        }

        // GET: MediaControlelr/Edit/5
        public ActionResult Edit(int id)
        {
			HttpContext.Session.SetString("path", Request.Path);
			 return View();
        }

        // POST: MediaControlelr/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            HttpContext.Session.SetString("path", Request.Path);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MediaControlelr/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MediaControlelr/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
			HttpContext.Session.SetString("path", Request.Path);
			try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        bool isLogged()
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                return true;
            }
            return false;
        }


        public ActionResult ChangeCulture(string lang)
        {
            string? returnUrl = HttpContext.Session.GetString("path") ?? "/Media/Index";
     
        
            List<string> cultures = new List<string>() {"ru", "en", "uk" };
            if(!cultures.Contains(lang)) 
            {
                lang = "uk";
            }


            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10);
            Response.Cookies.Append("lang", lang, option);
            return Redirect(returnUrl);
        }
    }
}
