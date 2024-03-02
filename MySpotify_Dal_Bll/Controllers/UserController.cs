using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MySpotify.BLL.DTO;
using MySpotify.BLL.Interfaces;
using MySpotify.DAL.Entities;
using MySpotify.Models;
using System.Security.Cryptography;
using System.Text;
using Status = MySpotify.BLL.DTO.Status;




namespace MySpotify.Controllers
{
    public class UserController : Controller
    {
        
        readonly IUserService _userService;
        readonly IGenreService _genreService;
        readonly IMediaService _mediaService;
        IHubContext<NotificationHub> _hubContext { get; }
        public UserController(IUserService userService, IGenreService genreService, IMediaService mediaService , IHubContext<NotificationHub> hubt)
        {
            _userService = userService;
            _genreService = genreService;
            _mediaService = mediaService;
            _hubContext = hubt;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel logon)
        {
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

        public async Task<ActionResult> Logout()
        {   var name = HttpContext.Session.GetString("FirstName");
            await SendMessage(name + " вышел из портала");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Media");
        }


        async Task SendMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync("displayMessage", message);
        }


        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationModel user)
        {
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
              await _userService.CreateUser(userDTO);
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
