using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models
{
    public class Region
    {
        public int X { get; set; }
        public int Z { get; set; }

        public IEnumerable<Chunk> Locations { get; set; }
        public string Path { get; internal set; }
    }
}
