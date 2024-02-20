using Microsoft.AspNetCore.Mvc;
using MySpotify.BLL.DTO;
using MySpotify.BLL.Interfaces;
using MySpotify.Filters;
using MySpotify.Models;
using System.Security.Cryptography;
using System.Text;




namespace MySpotify.Controllers
{
	[Culture]
	public class UserController : Controller
    {
        
        readonly IUserService _userService;
        readonly IGenreService _genreService;
        readonly IMediaService _mediaService;
        public UserController(IUserService userService, IGenreService genreService, IMediaService mediaService)
        {
            _userService = userService;
            _genreService = genreService;
            _mediaService = mediaService;
        }

        public ActionResult Login()
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel logon)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (ModelState.IsValid)
            {
                if ((await _userService.GetUserList()).Count() == 0)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                    return View(logon);
                }
                var users = (await _userService.GetUserList()).Where(a => a.Email == logon.Email);

                if (users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                    return View(logon);
                }
                var user = users.First();
                if (!user.Active)
                {
                    ModelState.AddModelError("", "Ожидание активации пользователя");
                    return View(logon);
                }

                if(!await _userService.isLogged(new UserDTO { Password = logon.Password , Id = user.Id}))
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                    return View(logon);
                }

                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetString("LastName", user.LastName);
                HttpContext.Session.SetString("Id", user.Id.ToString());
                return RedirectToAction("Index", "Media");
            }

            return View(logon);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetString("path", Request.Path);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Media");
        }




        public IActionResult Registration()
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(RegistrationModel user)
        {
            HttpContext.Session.SetString("path", Request.Path);
            UserDTO userDTO = new UserDTO()
            {
                Id = 0,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Active = false,
                Status = Status.user
            };
            if (ModelState.IsValid)
            {
               _userService.CreateUser(userDTO);
               return RedirectToAction("Login");
            }

            return View(user);
        }


        // GET: UserController
        public ActionResult Index()
        {
            if(!isLogged())
                return View("Index" , "User");
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
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
    }
}
