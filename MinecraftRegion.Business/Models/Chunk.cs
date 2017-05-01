using NBT.Business.Models.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models
{
    public class Chunk
    {
        public int Offset { get; set; }

        public byte SectorCount { get; set; }

        public int Timestamp { get; set; }

        public List<ChunkSector> Sectors { get; set; }

        public Chunk()
        {
            Sectors = new List<ChunkSector>();
        }
    }
}
