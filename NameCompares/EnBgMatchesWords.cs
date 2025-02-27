using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameCompares
{
    public class EnBgMatchesWords
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? EnWord { get; set; }

        [Required]
        public string? BgWord { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public DateTime DateTime { get; set; }


    }
}
