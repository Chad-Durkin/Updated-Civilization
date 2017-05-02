using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Civilization.Models
{
    [Table("PlayerGamePieces")]
    public class PlayerGamePiece
    {
        [Key]
        public int GamePieceId { get; set; }
        public virtual GamePiece GamePiece { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
