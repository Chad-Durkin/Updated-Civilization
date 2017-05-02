using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Civilization.Models;

namespace Civilization.ViewModels
{
    public class GameMapViewModel
    {
        public BoardPiece[] BoardPieces { get; set; }
        public virtual Player Player { get; set; }
    }
}
