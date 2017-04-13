using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.Tags
{
    class TAG_Float : NamedTAG
    {
        public override byte TagType { get { return 5; } }

        public float Value { get; internal set; }
    }
}
