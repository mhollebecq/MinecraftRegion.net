using MinecraftRegion.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business
{
    public class BlockReader
    {
        byte Nibble4(byte[] arr, int index)
        {
            return index % 2 == 0
                ? (byte)(arr[index / 2] & 0x0F)
                : (byte)((arr[index / 2] >> 4) & 0x0F);
        }

        public void ReadBlocks(Region region)
        {
            foreach (var chunk in region.Locations)
            {
                int xChunkInWorld = region.X + chunk.Sector.Level.XPos;
                int zChunkInWorld = region.Z + chunk.Sector.Level.ZPos;
                var sections = chunk.Sector.Level.Sections;
                foreach (var section in sections)
                {
                    for (int blockPos = 0; blockPos < section.Blocks.Length; blockPos++)
                    {
                        int xSection = blockPos % 16;
                        int xWorld = xChunkInWorld * 16 + xSection;
                        int zSection = (blockPos / 16) % 16;
                        int zWorld = zChunkInWorld * 16 + zSection;
                        int yWorld = (blockPos / 256) % 16;
                        byte BlockID_a = section.Blocks[blockPos];
                        byte BlockID_b = Nibble4(section.Add, blockPos);
                        short BlockID = (short)(BlockID_a + (BlockID_b << 8));
                        byte BlockData = Nibble4(section.Data, blockPos);
                        byte Blocklight = Nibble4(section.BlockLight, blockPos);
                        byte Skylight = Nibble4(section.SkyLight, blockPos);

                    }
                }
            }
        }
    }
}
