using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBT.Business.Models.Tags
{
    public class TAG_Byte : NamedTAG<byte>
    {
        public TAG_Byte() : base(TagType.Byte)
        {
        }
    }
}
