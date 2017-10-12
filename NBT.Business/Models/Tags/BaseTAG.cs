using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBT.Business.Models.Tags
{
    public abstract class BaseTAG
    {
        public TagType TagType { get; private set; }

        public BaseTAG(TagType tagType)
        {
            TagType = tagType;
        }
    }
}
