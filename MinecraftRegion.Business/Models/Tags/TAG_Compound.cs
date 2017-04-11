using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.Tags
{
    public class TAG_Compound : NamedTAG
    {

        public List<BaseTAG> Children { get; set; }

        public TAG_Compound()
        {
            Children = new List<BaseTAG>();
        }
    }
}
