using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Civilization.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Civilization.Models
{
    [Table("Queues")]
    public class Queue
    {
        [Key]
        public int id { get; set; }
        public virtual GamePiece GamePiece { get; set; }


        public static bool CheckForGamePiece(GamePiece gamePiece, CivilizationDbContext db)
        {
            Queue[] QueueList = db.Queues.ToArray();
            for (var i = 0; i < QueueList.Length; i++)
            {
                if (QueueList[i].GamePiece.Id == gamePiece.Id)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
