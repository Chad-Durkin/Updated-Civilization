using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Civilization.Models;
using Civilization.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Civilization.Controllers
{
    public class AccountController : Controller
    {
        private readonly CivilizationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, CivilizationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                User currentUser = await _userManager.FindByIdAsync(userId);
                Player currentPlayer = _db.Players.Include(player => player.User).FirstOrDefault(player => player.User.UserName == currentUser.UserName);

                BoardPiece.PopulateTable(_db);
                GameMapViewModel mapInfo = new GameMapViewModel
                {
                    BoardPieces = _db.BoardPieces.ToArray(),
                    Player = currentPlayer
                };
                return View(mapInfo);
            }
            else
            {
                return RedirectToAction("LoginOrRegister");
            }
        }

        public IActionResult LoginOrRegister()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new User { UserName = model.UserName };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            User newUser = await _userManager.FindByNameAsync(model.UserName);
            _db.Players.Add(new Player
            {
                Name = user.UserName,
                User = newUser
            });
            _db.SaveChanges();
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Account");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Account");
            }
            else
            {
                return View();
            }
        }
    }
}
