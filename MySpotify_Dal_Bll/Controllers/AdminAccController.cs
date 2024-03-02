using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySpotify.BLL.DTO;
using MySpotify.BLL.Interfaces;
using MySpotify.DAL.Entities;
using MySpotify.Models.AdminViewModels;

namespace MySpotify.Controllers
{
    public class AdminAccController : Controller
    {
        IUserService _userService;

        public AdminAccController(IUserService userService) => _userService = userService;
        // GET: AdminAccController
        public async Task<ActionResult> Index()
        {

            AdminAccIndexModel model = new AdminAccIndexModel() 
            {
            _users = await _userService.GetUserList()
             };

           
            return View(model);
        }

		public async Task<IActionResult> EditUser(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _userService.GetUser((int)id); //us with salt
			if (user == null)
			{
				return NotFound();
			}

			return PartialView("EditUser", user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> EditUser(int id, UserDTO user)
		{
			if (id != user.Id)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				try
				{
					await _userService.UpdateUser(user);
					
				}
				catch (DbUpdateConcurrencyException)
				{
					throw;
				}
				return Ok();
			}
			return NotFound();
		}


		public async Task<ActionResult> DeleteUser(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _userService.GetUser((int)id);
			if (user == null)
			{
				return NotFound();
			}
			return PartialView("DeleteUser", user);
		}


		[HttpPost]
		public async Task<IActionResult> DeleteUser(int id, UserDTO user)
		{


			if (id != user.Id)
			{
				return NotFound();
			}
			if (id>0)
			{
				try
				{
					await _userService.DeleteUser(id);
					
				}
				catch (DbUpdateConcurrencyException)
				{
					if (! await _userService.UserExist(id))
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
			return NotFound();
		}

	}
}
