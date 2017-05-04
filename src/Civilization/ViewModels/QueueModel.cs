using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Civilization.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Civilization.ViewModels
{
    public class QueueModel
    {
        public List<GamePiece> GamePieces { get; set; }

        public QueueModel(Queue[] gamePieces, CivilizationDbContext db)
        {
            List<GamePiece> newList = new List<GamePiece> { };
            for (var i = 0; i < gamePieces.Length; i++)
            {
                //GamePieces.Add(db.GamePieces.FirstOrDefault(model => model.Id == gamePieces[i].GamePiece.Id));
                newList.Add(gamePieces[i].GamePiece);
            }
            GamePieces = newList;
        }
    }
}
