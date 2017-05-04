using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Civilization.Models
{
    [Table("Resources")]
    public class Resource
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public virtual GamePiece GamePiece { get; set; }

        public static Resource[] RetrieveGamePieceResources(GamePiece gamePiece, CivilizationDbContext db)
        {
            Resource[] resources = db.Resources.Where(model => model.GamePiece.Id == gamePiece.Id).ToArray();

            return resources;
        }

        public static bool RemoveResourcesFromPlayer(Resource[] resources, Player player, CivilizationDbContext db)
        {
            bool success = true;
            var woodCost = 0;
            var stoneCost = 0;
            var metalCost = 0;
            var goldCost = 0;

            for (var i = 0; i < resources.Length; i++)
            {
                if(resources[i].Name == "Wood")
                {
                    if((player.Wood - resources[i].Cost) >= 0)
                    {
                        woodCost = resources[i].Cost;
                    }
                    else
                    {
                        success = false;
                    }
                }
                else if (resources[i].Name == "Stone")
                {
                    if ((player.Stone - resources[i].Cost) >= 0)
                    {
                        stoneCost = resources[i].Cost;
                    }
                    else
                    {
                        success = false;
                    }

                }
                else if (resources[i].Name == "Metal")
                {
                    if ((player.Metal - resources[i].Cost) >= 0)
                    {
                        metalCost = resources[i].Cost;
                    }
                    else
                    {
                        success = false;
                    }

                }
                else if (resources[i].Name == "Gold")
                {
                    if ((player.Gold - resources[i].Cost) >= 0)
                    {
                        goldCost = resources[i].Cost;
                    }
                    else
                    {
                        success = false;
                    }
                }
            }
            if(success == true)
            {
                player.Wood -= woodCost;
                player.Stone -= stoneCost;
                player.Metal -= metalCost;
                player.Gold -= goldCost;
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
            }

            return success;
        }
    }
}
