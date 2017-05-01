using System.Collections.Generic;

namespace MinecraftRegion.Business.Models
{
    public class Level
    {
        public sbyte[] Biomes { get; internal set; }
        public List<object> Entities { get; internal set; }
        public int[] HeightMap { get; internal set; }
        public long InhabitedTime { get; internal set; }
        public long LastUpdate { get; internal set; }
        public sbyte LightPopulated { get; internal set; }
        public List<object> Sections { get; internal set; }
        public sbyte TerrainPopuled { get; internal set; }
        public List<object> TileEntities { get; internal set; }
        public List<object> TileTicks { get; internal set; }
        public sbyte V { get; internal set; }
        public int XPos { get; internal set; }
        public int ZPos { get; internal set; }
    }
}