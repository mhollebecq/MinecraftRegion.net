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
                    int length = (int)(Math.Max(Math.Ceiling(Math.Log(section.Palette.Count, 2)), 4));
                    if (length % 4 != 0)
                    {

                    }
                    var mask = (1 << length) - 1;

                    int indicesInALong = 64 / length;
                    bool fitWell = 64 % length == 0;
                    for (int blockPos = 0; blockPos < 4096; blockPos++)
                    {
                        int longIndex = blockPos / (64 / length);
                        int indexInCurrentLong = blockPos * length - longIndex * 64;
                        
                        int paletteIndex = GetPaletteIndex(section, longIndex, indexInCurrentLong, length, mask, fitWell);
                        var paletteItem = section.Palette[paletteIndex];

                        int xSection = blockPos % 16;
                        int xWorld = xChunkInWorld * 16 + xSection;
                        int zSection = (blockPos / 16) % 16;
                        int zWorld = zChunkInWorld * 16 + zSection;
                        int yWorld = (blockPos / 256) + 16 * section.Y;

                        BlockType blockType = BlockTypes.GetBlock(paletteItem.Name);
                        blocks.Add(new Block()
                        {
                            BlockType = blockType,
                            XSection = xSection,
                            XWorld = xWorld,
                            YWorld = yWorld,
                            ZSection = zSection,
                            ZWorld = zWorld
                        });
                    }
                }
            }
            return blocks;
        }

        private static int GetPaletteIndex(LevelSection section, int longIndex, int indexInCurrentLong, int length, int mask, bool fitWell)
        {
            if (fitWell)
                return GetSimplePaletteIndex(section, longIndex, indexInCurrentLong, mask);
            else
            {
                if (indexInCurrentLong < 0)
                {
                    var smallPart = GetSimplePaletteIndex(section, longIndex-1, 64 + indexInCurrentLong, mask);
                    int bigPartMask = (1 << (length + indexInCurrentLong)) -1;

                    int paletteIndex = smallPart + ((int)(section.BlockStates[longIndex] & bigPartMask) << (indexInCurrentLong * -1));
                    return paletteIndex;
                }
                else
                    return GetSimplePaletteIndex(section, longIndex, indexInCurrentLong, mask);
            }
        }

        private static int GetSimplePaletteIndex(LevelSection section, int longIndex, int indexInCurrentLong, int mask)
        {
            return (int)((section.BlockStates[longIndex] >> indexInCurrentLong) & mask);
        }

        public List<Block> ReadBlocks(IEnumerable<Region> regions)
        {
            List<Block> blocks = new List<Block>();

            foreach (Region region in regions)
            {
                blocks.AddRange(ReadBlocks(region));
            }

            return blocks;
        }
    }
}
