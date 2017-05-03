using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Civilization.ViewModels
{
    public class PlayerMoving
    {
        public int CurrentTileId { get; set; }
        public int ClickedTileId { get; set; }
        public string ClickedTileRes { get; set; }
        public int WoodEq{ get; set; }
        public int StoneEq { get; set; }
        public int MetalEq { get; set; }
        public int GoldEq { get; set; }


        public PlayerMoving(int currentTileId, int clickedTileId, string clickedTileRes, int woodEq, int goldEq, int metalEq, int stoneEq)
        {
            CurrentTileId = currentTileId;
            ClickedTileId = clickedTileId;
            ClickedTileRes = clickedTileRes;
            WoodEq = woodEq;
            GoldEq = goldEq;
            MetalEq = metalEq;
            StoneEq = stoneEq;
        }

    }
}
