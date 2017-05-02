using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Civilization.Models
{
    [Table("BoardPieces")]
    public class BoardPiece
    {
        [Key]
        public int Id { get; set; }
        public string ResourceType { get; set; }
        public bool ResourceHere { get; set; }
        public bool BaseHere { get; set; }
        public bool PlayerHere { get; set; }

        public static void PopulateTable(CivilizationDbContext _db)
        {
            Random random = new Random();
            for (var i = 0; i < 100; i++)
            {
                BoardPiece newLand = new BoardPiece { BaseHere = false };
                var resource = random.Next(0, 100);
                if (resource < 20)
                {
                    newLand.ResourceHere = true;
                    newLand.ResourceType = "Wood";
                }
                else if (resource < 30)
                {
                    newLand.ResourceHere = true;
                    newLand.ResourceType = "Metal";
                }
                else if (resource < 45)
                {
                    newLand.ResourceHere = true;
                    newLand.ResourceType = "Stone";
                }
                else if (resource < 50)
                {
                    newLand.ResourceHere = true;
                    newLand.ResourceType = "Gold";
                }
                else
                {
                    newLand.ResourceHere = false;
                    newLand.ResourceType = "None";
                }
                //Set Player to starting piece
                if (i == 55)
                {
                    newLand.PlayerHere = true;
                }
                else
                {
                    newLand.PlayerHere = false;
                }
                _db.BoardPieces.Add(newLand);
                _db.SaveChanges();
            }
        }
    }
}
