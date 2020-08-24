using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinecraftRegion.Business.Models;

namespace MinecraftRegion.Business.Tests
{
    [TestClass]
    public class BlockReaderTests
    {
        [TestMethod]
        public void ReadBlocksTest()
        { 
            List<long> longs = new List<long>();
            longs.Add(0);
            longs.Add(1);
            for(int i = 0; i < 13; i++)
            {
                int paletteIndex =  16;
                longs[0] = longs[0] + ((long)paletteIndex << i * 5);
            }
            Region region = new Region();
            region.Locations = new List<Chunk>();
            Chunk chunk = new Chunk();
            chunk.Sector = new ChunkSector()
            {
                DataVersion = 1000,
                Level = new Level()
                {
                    Sections = new List<LevelSection>()
                    {
                        new LevelSection()
                        {
                            BlockStates = longs.ToArray(),
                            Palette = new List<Palette>()
                            {
                                new Palette(){Name="minecraft:air"},
                                new Palette(){Name="minecraft:stone"},
                                new Palette(){Name="minecraft:granite"},
                                new Palette(){Name="minecraft:polished_granite"},
                                new Palette(){Name="minecraft:diorite"},
                                new Palette(){Name="minecraft:polished_diorite"},
                                new Palette(){Name="minecraft:andesite"},
                                new Palette(){Name="minecraft:polished_andesite"},
                                new Palette(){Name="minecraft:grass_block"},
                                new Palette(){Name="minecraft:dirt"},
                                new Palette(){Name="minecraft:podzol"},
                                new Palette(){Name="minecraft:coarse_dirt"},
                                new Palette(){Name="minecraft:cobblestone"},
                                new Palette(){Name="minecraft:oak_planks"},
                                new Palette(){Name="minecraft:planks"},
                                new Palette(){Name="minecraft:sapling"},
                                new Palette(){Name="minecraft:bedrock"},
                                new Palette(){Name="minecraft:flowing_water"},
                                new Palette(){Name="minecraft:water"},
                                new Palette(){Name="minecraft:flowing_lava"},
                                new Palette(){Name="minecraft:lava"},
                                new Palette(){Name="minecraft:sand" }
                            }
                        }
                    }
                }
            };
            region.Locations.Add(chunk);
            BlockReader reader = new BlockReader();
            var blocks = reader.ReadBlocks(region);
        }
    }
}
