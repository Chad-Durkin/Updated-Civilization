using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Civilization.Models
{
    [Table("Environments")]
    public class Environment
    {
        [Key]
        public string ResourceType { get; set; }
        public bool ResourceHere { get; set; }
        public int BaseId { get; set; }
    }
}
