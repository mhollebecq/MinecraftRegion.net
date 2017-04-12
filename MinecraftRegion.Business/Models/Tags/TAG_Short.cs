using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.Tags
{
    public class TAG_Short : NamedTAG
    {
        public override byte TagType { get { return 2; } }

        public short Value { get; set; }
    }
}
