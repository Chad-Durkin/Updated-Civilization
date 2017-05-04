using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Civilization.Models;

namespace Civilization.ViewModels
{
    public class GamePieceMod
    {
        public string GPName { get; set; }
        public string GPType { get; set; }
        public int GPTurnCost { get; set; }

        public GamePieceMod(string gpName, string gpType, int gpTurnCost)
        {
            GPName = gpName;
            GPType = gpType;
            GPTurnCost = gpTurnCost;
        }

    }
}
