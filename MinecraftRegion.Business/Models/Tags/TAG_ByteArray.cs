using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.Tags
{
    class TAG_ByteArray : NamedTAG
    {
        public override byte TagType { get { return 7; } }

        public sbyte[] Values { get; set; }
    }
}
