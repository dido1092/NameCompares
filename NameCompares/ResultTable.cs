using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameCompares
{
    public class ResultTable
    {
        public ResultTable()
        {

        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string LatWord { get; set; }

        [Required]
        public string BgWord { get; set; }

        [Required]
        public int LatWordLength { get; set; }

        [Required]
        public int BgWordLength { get; set; }


        [Required]
        public string Comparison { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public int? LettRelationsId { get; set; }
        public LettRelations? LettRelations { get; set; }

        //public ICollection<LettRelations> LetRelatId { get; set; }
    }
}
