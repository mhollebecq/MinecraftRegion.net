using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.BlockEntities
{
    public class SignEntity : BlockEntity
    {
        public SignEntity()
        {
            Id = "minecraft:sign";
        }

        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Text4 { get; set; }
    }
}
