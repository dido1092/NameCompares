using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameCompares
{
    public class WorldNames
    {
        public WorldNames()
        {
                
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string WorldName { get; set; }

        public string Description { get; set; }


        public int Length { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
