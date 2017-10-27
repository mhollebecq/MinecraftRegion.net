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
        ///Blocks: 4096 bytes of block IDs defining the terrain. 8 bits per block, plus the bits from the below Add tag.
        /// </summary>
        public byte[] Blocks { get; set; }

        /// <summary>
        /// May not exist. 2048 bytes of additional block ID data. The value to add to (combine with) the above block ID to form the true block ID in the range 0 to 4095. 4 bits per block. Combining is done by shifting this value to the left 8 bits and then adding it to the block ID from above.
        /// </summary>
        public byte[] Add { get; set; }

        /// <summary>
        /// Data: 2048 bytes of block data additionally defining parts of the terrain. 4 bits per block.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// BlockLight: 2048 bytes recording the amount of block-emitted light in each block. Makes load times faster compared to recomputing at load time. 4 bits per block.
        /// </summary>
        public byte[] BlockLight { get; set; }

        /// <summary>
        /// SkyLight: 2048 bytes recording the amount of sunlight or moonlight hitting each block. 4 bits per block.
        /// </summary>
        public byte[] SkyLight { get; set; }
    }
}
