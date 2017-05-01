using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBT.Business.Models.Tags
{
    public abstract class NamedTAG<T> : TypedTAG<T>, INamedTag
    {
        public string Name { get; set; }


        public NamedTAG(TagType tagType) : base(tagType)
        {

        }

        public override string ToString()
        {
            return string.Format("{0} - Value : {1}", Name, Value);
        }

    }
}
