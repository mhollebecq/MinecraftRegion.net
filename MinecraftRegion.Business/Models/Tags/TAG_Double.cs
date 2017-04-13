using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.Tags
{
    class TAG_Double : NamedTAG
    {
        public override byte TagType { get { return 6; } }

        public double Value { get; internal set; }
    }
}
