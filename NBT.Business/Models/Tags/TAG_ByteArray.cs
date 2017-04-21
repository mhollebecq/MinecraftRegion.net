using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBT.Business.Models.Tags
{
    class TAG_ByteArray : NamedTAG<sbyte[]>
    {
        public TAG_ByteArray() : base(TagType.ByteArray)
        {
        }
    }
}
