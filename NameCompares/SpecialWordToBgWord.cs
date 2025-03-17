using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameCompares
{
    public class SpecialWordToBgWord
    {
        // This Class Is NOT Table
        public int Id { get; set; }
        public string? SpecialWord { get; set; }

        public int SpecialWordLength { get; set; }

        public string? BgWord { get; set; }   
        
        public int BgWordLength { get; set; }

        public override bool Equals(object? obj)
        {
            var word = obj as SpecialWordToBgWord;
            return word!.SpecialWord == this.SpecialWord;
        }
        public override int GetHashCode()
        {
            return this.SpecialWordLength;
        }
    }
}
