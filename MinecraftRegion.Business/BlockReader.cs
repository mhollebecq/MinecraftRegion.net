﻿using MinecraftRegion.Business.Models;
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

        public List<Block> ReadBlocks(Region region)
        {
            List<Block> blocks = new List<Block>();
            foreach (var chunk in region.Locations)
            {
                int xChunkInWorld = region.X * 32 + chunk.Sector.Level.XPos;
                int zChunkInWorld = region.Z * 32 + chunk.Sector.Level.ZPos;
                var sections = chunk.Sector.Level.Sections;
                foreach (var section in sections)
                {
                    //for (int blockPos = 0; blockPos < section.Blocks.Length; blockPos++)
                    //{
                    //    int xSection = blockPos % 16;
                    //    int xWorld = xChunkInWorld * 16 + xSection;
                    //    int zSection = (blockPos / 16) % 16;
                    //    int zWorld = zChunkInWorld * 16 + zSection;
                    //    int yWorld = (blockPos / 256) + 16 * section.Y;
                    //    byte BlockID_a = section.Blocks[blockPos];
                    //    byte BlockID_b = (byte)0;
                    //    if (section.Data != null)
                    //        BlockID_b = Nibble4(section.Data, blockPos);
                    //    short BlockID = (short)(BlockID_a + (BlockID_b << 8));
                    //    //byte BlockData = Nibble4(section.Data, blockPos);
                    //    //byte Blocklight = Nibble4(section.BlockLight, blockPos);
                    //    //byte Skylight = Nibble4(section.SkyLight, blockPos);
                    //    BlockType blockType = BlockTypes.GetBlock(BlockID_a, BlockID_b);
                    //    blocks.Add(new Block()
                    //    {
                    //        BlockID = BlockID,
                    //        BlockID_a = BlockID_a,
                    //        BlockID_b = BlockID_b,
                    //        BlockType = blockType,
                    //        XSection = xSection,
                    //        XWorld = xWorld,
                    //        YWorld = yWorld,
                    //        ZSection = zSection,
                    //        ZWorld = zWorld
                    //    });
                    //}
                }
            }
            return blocks;
        }

        public List<Block> ReadBlocks(IEnumerable<Region> regions)
        {
            List<Block> blocks = new List<Block>();

            foreach(Region region in regions)
            {
                blocks.AddRange(ReadBlocks(region));
            }

            return blocks;
        }
    }
}
