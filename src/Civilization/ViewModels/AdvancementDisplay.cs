using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Civilization.Models;

namespace Civilization.ViewModels
{
    public class AdvancementDisplay
    {
        public GamePiece[] GamePieces { get; set; }
        public Resource[] Resources { get; set; }
        public virtual Player Player { get; set; }
        public AdvancementDisplay(GamePiece[] gamePieces, Resource[] resources, Player player)
        {
            GamePieces = gamePieces;
            Resources = resources;
            Player = player;
        }
    }
}
