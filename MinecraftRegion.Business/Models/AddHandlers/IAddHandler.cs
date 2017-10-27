using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models
{
    interface IAddHandler
    {
        bool CheckBlockId(byte blockId);

        bool CheckBlockAdd(byte add);
    }
}
