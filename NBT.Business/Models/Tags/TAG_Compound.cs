using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBT.Business.Models.Tags
{
    public class TAG_Compound : NamedTAG<List<BaseTAG>>
    {
        public TAG_Compound() : base(TagType.Compound)
        {
            Value = new List<BaseTAG>();
        }
    }
}
