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
                GamePiece.PopulateTech(_db);
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
            
        public async Task<IActionResult> LogOutAndClearDb()
        {
            await _signInManager.SignOutAsync();
            await _db.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE [BoardPieces]");
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            User currentUser = await _userManager.FindByIdAsync(userId);
            Player currentPlayer = _db.Players.Include(player => player.User).FirstOrDefault(player => player.User.UserName == currentUser.UserName);
            currentPlayer.AvailableMoves = 5;
            currentPlayer.Wood = 10;
            currentPlayer.Metal = 0;
            currentPlayer.Stone = 2;
            currentPlayer.Gold = 0;
            _db.Entry(currentPlayer).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("LoginOrRegister");
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

        public async Task<IActionResult> Move(int clickedTileId)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            User currentUser = await _userManager.FindByIdAsync(userId);
            Player currentPlayer = _db.Players.FirstOrDefault(player => player.Name == currentUser.UserName);
            BoardPiece currentPiece = _db.BoardPieces.FirstOrDefault(piece => piece.PlayerHere == true);
            BoardPiece clickedPiece = _db.BoardPieces.FirstOrDefault(piece => piece.Id == clickedTileId);
            PlayerMoving firstMove = new PlayerMoving(0, 0, null, 0, 0, 0, 0, 0, false);
            if(currentPlayer.AvailableMoves > 0)
            {
                currentPlayer.AvailableMoves -= 1;
                if (currentPiece.Id == (clickedPiece.Id + 1) || currentPiece.Id == (clickedPiece.Id - 1) || currentPiece.Id == (clickedPiece.Id + 10) || currentPiece.Id == (clickedPiece.Id - 10))
                {
                currentPiece.PlayerHere = false;
                currentPlayer.AddResource(clickedPiece.ResourceType);
                PlayerMoving successMove = new PlayerMoving(currentPiece.Id, clickedPiece.Id, clickedPiece.ResourceType, currentPlayer.Wood, currentPlayer.Gold, currentPlayer.Metal, currentPlayer.Stone, currentPlayer.AvailableMoves, true);
                    firstMove = successMove;
                //currentPiece.ResourceHere = false;
                //currentPiece.ResourceType = "None";
                clickedPiece.PlayerHere = true;
                //currentPlayer.AddResource(clickedPiece.ResourceType);
                clickedPiece.ResourceHere = false;
                clickedPiece.ResourceType = "None";

                //_db.Entry(currentPlayer).State = EntityState.Modified;
                _db.Entry(currentPiece).State = EntityState.Modified;
                _db.Entry(clickedPiece).State = EntityState.Modified;
                _db.SaveChanges();

                }
            }

            return Json(firstMove);
        }

        public IActionResult AdvancementDisplay(int id)
        {
            //Player passedInPlayer = player;
            //var passedId = id;
            Player player = _db.Players.FirstOrDefault(targ => targ.Id == id);
            //PlayerGamePiece[] GamePieces = _db.PlayerGamePieces.Where(checkPlayer => checkPlayer.PlayerId == player.Id).ToArray();
            //AdvancementDisplay display = new AdvancementDisplay(GamePieces, player.Name, player.Wood, player.Metal, player.Stone, player.Gold, player);
            return Json(player);
        }
        public IActionResult AllAdvancement(int id)
        {
            Player player = _db.Players.FirstOrDefault(targ => targ.Id == id);
            Resource[] Resources = _db.Resources.ToArray();
            GamePiece[] GamePieces = _db.GamePieces.ToArray();
            AdvancementDisplay display = new AdvancementDisplay(GamePieces, Resources, player);
            return Json(display);
        }

        public IActionResult EndTurn(int id)
        {
            Player player = _db.Players.FirstOrDefault(targ => targ.Id == id);
            player.AvailableMoves = 5;
            _db.Entry(player).State = EntityState.Modified;
            _db.SaveChanges();
            return Content(5.ToString(), "text/plain");
        }

        public IActionResult ShowQueCounter(int id)
        {
            Player player = _db.Players.FirstOrDefault(targ => targ.Id == id);
            GamePiece choseFire = _db.GamePieces.FirstOrDefault(gp => gp.Id == 1);
            GamePieceMod queEquipUtility = new GamePieceMod(choseFire.Name, choseFire.Type, choseFire.TurnCost);
            return Json(queEquipUtility);
        }

        public IActionResult AddToQueue(int id)
        {
            GamePiece gamePiece = _db.GamePieces.FirstOrDefault(model => model.Id == id);
            Queue queuePiece = new Queue { GamePiece = gamePiece };
            _db.Queues.Add(queuePiece);
            _db.SaveChanges();
            Queue[] QueueList = _db.Queues.ToArray();
            //QueueModel display = new QueueModel(QueueList, _db);
            return Json(QueueList);
        }
    }
}
