using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameCompares
{
    public class BgWords
    {
        public BgWords()
        {
               
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string BgWord { get; set; }

        public string Description { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public override bool Equals(object? obj)
        {
            var bgWord = obj as BgWords;
            return bgWord!.BgWord == this.BgWord;
        }
        public override int GetHashCode()
        {
            return this.BgWord.Length;
        }
    }
}
