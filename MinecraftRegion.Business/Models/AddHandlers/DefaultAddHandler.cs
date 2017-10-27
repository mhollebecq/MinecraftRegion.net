using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.AddHandlers
{
    class DefaultAddHandler : IAddHandler
    {
        public bool CheckBlockAdd(byte add)
        {
            throw new NotImplementedException();
        }

        public bool CheckBlockId(byte blockId)
        {
            return true;
        }
    }
}
