using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.Tags
{
    public class TAG_List : NamedTAG<List<object>>
    {
        public sbyte TagId { get; set; }

        public int Size { get; set; }

        public TAG_List():base(TagType.List)
        {
            Value = new List<object>();
        }
    }
}
