using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Civilization.Models
{
    [Table("GamePieces")]
    public class GamePiece
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
        public int TurnCost { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
        public virtual ICollection<Requirement> Requirements { get; set; }
        public virtual ICollection<PlayerGamePiece> PlayerGamePieces { get; set; }

        public static void PopulateTech(CivilizationDbContext db)
        {
            GamePiece inventFire = new GamePiece { Name = "Discover Fire", Type = "Tech", Count = 1, TurnCost = 2 };
            Resource fireResource1 = new Resource { Name = "Wood", Cost = 2, GamePiece = inventFire };
            Resource fireResource2 = new Resource { Name = "Stone", Cost = 1, GamePiece = inventFire };
            db.GamePieces.Add(inventFire);
            db.Resources.Add(fireResource1);
            db.Resources.Add(fireResource2);
            db.SaveChanges();
        }
    }
}
