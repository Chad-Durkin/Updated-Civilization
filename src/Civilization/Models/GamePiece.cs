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
        public string Resource { get; set; }
        public int Cost { get; set; }
        public virtual ICollection<Requirement> Requirements { get; set; }
        public virtual ICollection<PlayerGamePiece> PlayerGamePieces { get; set; }
    }
}
