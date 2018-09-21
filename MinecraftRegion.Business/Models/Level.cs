using MinecraftRegion.Business.Models.BlockEntities;
using System.Collections.Generic;

namespace MinecraftRegion.Business.Models
{
    public class Level
    {
        public int[] Biomes { get; internal set; }
        public List<object> Entities { get; internal set; }
        public int[] HeightMap { get; internal set; }
        public long InhabitedTime { get; internal set; }
        public long LastUpdate { get; internal set; }
        public byte LightPopulated { get; internal set; }
        public List<LevelSection> Sections { get; internal set; }
        public byte TerrainPopulated { get; internal set; }
        public List<BlockEntity> TileEntities { get; internal set; }
        public List<object> TileTicks { get; internal set; }
        public byte V { get; internal set; }
        public int XPos { get; internal set; }
        public int ZPos { get; internal set; }
    }
}