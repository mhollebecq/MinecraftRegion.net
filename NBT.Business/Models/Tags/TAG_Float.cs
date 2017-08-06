using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBT.Business.Models.Tags
{
    public class TAG_Float : NamedTAG<float>
    {
        public TAG_Float() : base(TagType.Float)
        {

        }
    }
}
