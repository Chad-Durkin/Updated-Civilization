using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Civilization.Models
{
    [Table("Requirements")]
    public class Requirement
    {
        [Key]
        public int Id { get; set; }
        public virtual GamePiece GamePiece { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
