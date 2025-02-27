using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameCompares
{
    public class WorldBgNames
    {
        //This Class Is NOT Table
        public WorldBgNames()
        {
            
        }
        public int Id { get; set; }

        public string Name { get; set; }
        public string? WorldName { get; set; }

        public int WorldNameLength { get; set; }

        public string? BgName { get; set; }

        public int BgNameLength { get; set; }

        public int LetRelatId { get; set; }

        public string Comparison { get; set; }
        public override bool Equals(object? obj)
        {
            var names = obj as WorldBgNames;
            return names!.WorldName == this.BgName;
        }
        public override int GetHashCode()
        {
            return this.WorldNameLength;
        }
    }
}
