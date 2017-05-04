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

    }
}
