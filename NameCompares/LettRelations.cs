using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameCompares
{
    public class LettRelations
    {
        public LettRelations()
        {
            this.Results = new List<ResultTable>(); 
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Letters { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public ICollection<ResultTable> Results { get; set; }
    }
}
