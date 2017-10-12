using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBT.Business.Models.Tags
{
    public class TAG_List : NamedTAG<List<object>>
    {
        public sbyte TagId { get; set; }

        public int Size { get; set; }

        public TAG_List():base(TagType.List)
        {
            Value = new List<object>();
        }

        public override string ToString()
        {
            return string.Format("{0} - TagId : {1} - Size : {2}", Name, TagId, Size);
        }
    }
}
