using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBT.Business.Models.Tags
{
    class TAG_IntArray : NamedTAG<int[]>
    {
        public TAG_IntArray():base(TagType.IntArray)
        {

        }
    }
}
