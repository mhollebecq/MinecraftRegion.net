using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.Tags
{
    public abstract class BaseTAG
    {
        public TagType TagType { get; protected set; }

        public BaseTAG(TagType tagType)
        {
            TagType = tagType;
        }
    }
}
