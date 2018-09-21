using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models
{
    public class LevelSection
    {
        public Guid Guid { get; }

        public LevelSection()
        {
            Guid = Guid.NewGuid();
        }

        //       Sections: List of Compound tags, each tag is a sub-chunk of sorts.

        //       An individual Section.
        
        /// <summary>
        /// Y: The Y index(not coordinate) of this section.Range 0 to 15 (bottom to top), with no duplicates but some sections may be missing if empty.
        /// </summary>
        public byte Y { get; set; }

        /// <summary>
        /// BlockLight: 2048 bytes recording the amount of block-emitted light in each block. Makes load times faster compared to recomputing at load time. 4 bits per block.
        /// </summary>
        public byte[] BlockLight { get; set; }

        /// <summary>
        /// SkyLight: 2048 bytes recording the amount of sunlight or moonlight hitting each block. 4 bits per block.
        /// </summary>
        public byte[] SkyLight { get; set; }


        public List<Palette> Palette { get; internal set; }

        public long[] BlockStates { get; internal set; }
    }
}
