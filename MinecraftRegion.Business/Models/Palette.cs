using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models
{
    public class Palette
    {
        public string Name { get; set; }

        public PaletteProperties Properties { get; set; }
    }

    public class PaletteProperties
    {
        public string Name { get; set; }
    }
}
