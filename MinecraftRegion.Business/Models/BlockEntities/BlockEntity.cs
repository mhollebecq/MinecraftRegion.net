using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.BlockEntities
{
    public abstract class BlockEntity
    {
        public string Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
