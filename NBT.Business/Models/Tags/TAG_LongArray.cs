using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBT.Business.Models.Tags
{
    public class TAG_LongArray : NamedTAG<long[]>
    {
        public TAG_LongArray() : base(TagType.LongArray)
        {

        }
    }
}
