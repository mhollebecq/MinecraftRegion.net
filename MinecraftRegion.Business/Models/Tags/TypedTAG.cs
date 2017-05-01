using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.Tags
{
    public class TypedTAG<T> : BaseTAG
    {
        public T Value { get; set; }

        public TypedTAG(TagType tagType):base(tagType)
        {
        }
    }
}
