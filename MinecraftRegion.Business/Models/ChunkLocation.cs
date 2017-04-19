using MinecraftRegion.Business.Models.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models
{
    public class ChunkLocation
    {
        public int Offset { get; set; }

        public byte SectorCount { get; set; }

        public int Timestamp { get; set; }

        public List<BaseTAG> Sectors { get; set; }
    }
}
