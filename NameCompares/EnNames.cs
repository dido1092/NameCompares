using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameCompares
{
    public class EnNames
    {
        public EnNames()
        {

        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string EnName { get; set; }

        public string Description { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public override bool Equals(object? obj)
        {
            var enName = obj as EnNames;
            return enName!.EnName == this.EnName;
        }
        public override int GetHashCode()
        {
            return this.EnName.Length;
        }
    }
}
