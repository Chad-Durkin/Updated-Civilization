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
    public class GameController : Controller
    {
        private readonly CivilizationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public GameController(UserManager<User> userManager, SignInManager<User> signInManager, CivilizationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public async Task<IActionResult> Move(int clickedTileId)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            User currentUser = await _userManager.FindByIdAsync(userId);
            Player currentPlayer = _db.Players.FirstOrDefault(player => player.Name == currentUser.UserName);
            BoardPiece currentPiece = _db.BoardPieces.FirstOrDefault(piece => piece.PlayerHere == true);
            BoardPiece clickedPiece = _db.BoardPieces.FirstOrDefault(piece => piece.Id == clickedTileId);
            currentPiece.PlayerHere == false;
            currentPiece.ResourceHere == false;
            currentPiece.ResourceType == "None";
            clickedPiece.PlayerHere == true;
            clickedPiece.
            _db.Entry(currentPlayer).State = EntityState.Modified;
            _db.SaveChanges()
        }
    }
}
