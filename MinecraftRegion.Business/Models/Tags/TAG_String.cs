using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.Tags
{
    class TAG_String : NamedTAG
    {
        public override byte TagType { get { return 8; } }

        public string Value { get; set; }
    }
}
