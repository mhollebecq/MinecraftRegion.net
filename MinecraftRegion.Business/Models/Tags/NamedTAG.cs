using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.Tags
{
    public abstract class NamedTAG<T> : TypedTAG<T>
    {
        public string Name { get; set; }


        public NamedTAG(TagType tagType) : base(tagType)
        {

        }

    }
}
