using MinecraftRegion.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business
{
    public class BlockWriter
    {
        public List<Region> WriteBlocks(IEnumerable<Block> blocks)
        {
            List<Region> regions = new List<Region>();
            return WriteBlocks(blocks, regions);
        }

        public List<Region> WriteBlocks(IEnumerable<Block> blocks, List<Region> regions)
        {
            var blocksByChunkInWorldCoord = blocks.Select(block =>
                                                    (xChunkInWorld: (block.XWorld) / 16,
                                                    zChunkInWorld: (block.ZWorld) / 16,
                                                    block))
                                                    .GroupBy(b => (b.xChunkInWorld, b.zChunkInWorld), b=> b.block);

            var blocksByRegionCoord = blocksByChunkInWorldCoord.Select(g =>
                                                                (regionX: g.Key.xChunkInWorld / 32,
                                                                regionZ: g.Key.zChunkInWorld / 32,
                                                                byRegionCoordGroup: g))
                                                                .GroupBy(b=>(b.regionX, b.regionZ), b=>b.byRegionCoordGroup);

            foreach(var region in blocksByRegionCoord)
            {
                int regionX = region.Key.regionX;
                int regionZ = region.Key.regionZ;
                Region currentRegion = regions.FirstOrDefault(r => r.X == regionX && r.Z == regionZ);
                if (currentRegion == null)
                {
                    currentRegion = new Region();
                    currentRegion.X = regionX;
                    currentRegion.Z = regionZ;
                    currentRegion.Locations = new List<Chunk>();
                    regions.Add(currentRegion);
                }

                foreach(var chunckInWorld in region)
                {
                    int xChunkInWorld = chunckInWorld.Key.xChunkInWorld;
                    int zChunkInWorld = chunckInWorld.Key.zChunkInWorld;
                    foreach (var blockByLvl in chunckInWorld.Select(block => (xInSection: block.XWorld - 16 * xChunkInWorld,
                                                 zInSection: block.ZWorld - 16 * zChunkInWorld,
                                                 ySection: block.YWorld / 16,
                                                 yInSection: block.YWorld % 16,
                                                 lvlXpos: xChunkInWorld - 32 * regionX,
                                                 lvlZpos: zChunkInWorld - 32 * regionZ,
                                                 block))
                                                .GroupBy(block => (block.lvlXpos, block.lvlZpos)))
                                                
                    {
                        int lvlXpos = blockByLvl.Key.lvlXpos;
                        int lvlZpos = blockByLvl.Key.lvlZpos;
                        Chunk currentChunk = currentRegion.Locations.FirstOrDefault(c => c.Sector.Level.XPos == lvlXpos && c.Sector.Level.ZPos == lvlZpos);
                        if (currentChunk == null)
                        {
                            currentChunk = new Chunk();
                            currentChunk.Timestamp = (int)(DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
                            currentChunk.Sector = new ChunkSector();
                            currentChunk.Sector.DataVersion = 1628;
                            currentChunk.Sector.Level = new Level();
                            currentChunk.Sector.Level.Biomes = new int[256];
                            currentChunk.Sector.Level.Entities = new List<object>();
                            currentChunk.Sector.Level.TileEntities = new List<Models.BlockEntities.BlockEntity>();
                            currentChunk.Sector.Level.HeightMap = new int[256];
                            currentChunk.Sector.Level.V = 1;
                            currentChunk.Sector.Level.XPos = lvlXpos;
                            currentChunk.Sector.Level.ZPos = lvlZpos;
                            currentChunk.Sector.Level.Status = "fullchunk";
                            currentChunk.Sector.Level.Sections = new List<LevelSection>();
                            currentChunk.Sector.Level.LiquidsToBeTicked = new List<object>();
                            currentChunk.Sector.Level.ToBeTicked = new List<object>();
                            currentChunk.Sector.Level.PostProcessing = new List<object>();
                            currentChunk.Sector.Level.LiquidTicks = new List<object>();
                            for (int i = 0; i < 16; i++)
                            {
                                currentChunk.Sector.Level.LiquidsToBeTicked.Add(new List<object>());
                                currentChunk.Sector.Level.ToBeTicked.Add(new List<object>());
                                currentChunk.Sector.Level.PostProcessing.Add(new List<object>());
                            }
                            currentRegion.Locations.Add(currentChunk);
                        }
                        foreach (var blockGrp in blockByLvl.GroupBy(obj => obj.ySection, block => (block.block, block.xInSection, block.zInSection, block.yInSection)))
                        {
                            int ySection = blockGrp.Key;
                            LevelSection currentSection = currentChunk.Sector.Level.Sections.FirstOrDefault(s => s.Y == ySection);
                            if (currentSection == null)
                            {
                                currentSection = new LevelSection();
                                currentSection.Y = (byte)ySection;
                                currentSection.BlockLight = new byte[2048];
                                //currentSection.Blocks = new byte[4096];
                                //currentSection.Data = new byte[2048];
                                currentSection.SkyLight = new byte[2048];
                                //currentSection.Add = new byte[2048];
                                for (int i = 0; i < 2048; i++)
                                {
                                    //currentSection.BlockLight[i] = 255;
                                    currentSection.SkyLight[i] = 255;
                                }

                                currentChunk.Sector.Level.Sections.Add(currentSection);
                            }
                            var allBlockTypes = (new string[] { BlockTypes.Air.Name })
                                        .Concat(blockGrp.Select(b => b.block.BlockType.Name)).Distinct().ToList();

                            currentSection.Palette = allBlockTypes.Select(t => new Palette() { Name = t }).ToList();
                            int length = (int)(Math.Max(Math.Ceiling(Math.Log(currentSection.Palette.Count, 2)), 4));
                            if (length % 4 != 0)
                                throw new NotImplementedException("Come back later, we only handle factors of 64");

                            int indicesInALong = 64 / length;
                            bool fitWell = 64 % length == 0;

                            currentSection.BlockStates = new long[4096 / indicesInALong];
                            foreach (var blockAndCoord in blockGrp)
                            {
                                int yInSection = blockAndCoord.yInSection;
                                int xInSection = blockAndCoord.xInSection;
                                int zInSection = blockAndCoord.zInSection;
                                int blockPos = 256 * yInSection + 16 * zInSection + xInSection;
                                Block block = blockAndCoord.block;

                                //for (int blockPos = 0; blockPos < 4096; blockPos++)
                                //{
                                int longIndex = blockPos / (64 / length);
                                int indexInCurrentLong = blockPos * length - longIndex * 64;
                                var mask = (int)Math.Pow(length, 2) - 1;
                                var paletteItem = currentSection.Palette.First(p => p.Name == block.BlockType.Name);
                                var paletteIndex = currentSection.Palette.IndexOf(paletteItem);
                                var toAdd = ((long)paletteIndex << indexInCurrentLong);
                                currentSection.BlockStates[longIndex] = currentSection.BlockStates[longIndex] | toAdd;
                                // (int)((currentSection.BlockStates[longIndex] >> indexInCurrentLong) & mask);

                                //    int xSection = blockPos % 16;
                                //    int xWorld = xChunkInWorld * 16 + xSection;
                                //    int zSection = (blockPos / 16) % 16;
                                //    int zWorld = zChunkInWorld * 16 + zSection;
                                //    int yWorld = (blockPos / 256) + 16 * section.Y;
                                //block.
                            }
                        }
                    }
                                                
                }
            }
            /*foreach (Block block in blocks)
            {

                //Chunk are 16 blocks wide
                int xChunkInWorld = (block.XWorld) / 16;
                int zChunkInWorld = (block.ZWorld) / 16;

                //block coordinates in chunk
                int xInSection = block.XWorld - 16 * xChunkInWorld;
                int zInSection = block.ZWorld - 16 * zChunkInWorld;
                int ySection = block.YWorld / 16;
                int yInSection = block.YWorld % 16;

                //region coordinates. Up to 32*32 chunk in one region
                int regionX = xChunkInWorld / 32;
                int regionZ = zChunkInWorld / 32;

                //chunkCoordinate in the world
                int lvlXpos = xChunkInWorld - 32 * regionX;
                int lvlZpos = zChunkInWorld - 32 * regionZ;

                Region currentRegion = regions.FirstOrDefault(r => r.X == regionX && r.Z == regionZ);
                if (currentRegion == null)
                {
                    currentRegion = new Region();
                    currentRegion.X = regionX;
                    currentRegion.Z = regionZ;
                    currentRegion.Locations = new List<Chunk>();
                    regions.Add(currentRegion);
                }
                Chunk currentChunk = currentRegion.Locations.FirstOrDefault(c => c.Sector.Level.XPos == lvlXpos && c.Sector.Level.ZPos == lvlZpos);
                if (currentChunk == null)
                {
                    currentChunk = new Chunk();
                    currentChunk.Timestamp = (int)(DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
                    currentChunk.Sector = new ChunkSector();
                    currentChunk.Sector.DataVersion = 1343;
                    currentChunk.Sector.Level = new Level();
                    currentChunk.Sector.Level.Biomes = new int[256];
                    currentChunk.Sector.Level.Entities = new List<object>();
                    currentChunk.Sector.Level.TileEntities = new List<Models.BlockEntities.BlockEntity>();
                    currentChunk.Sector.Level.HeightMap = new int[256];
                    currentChunk.Sector.Level.V = 1;
                    currentChunk.Sector.Level.XPos = lvlXpos;
                    currentChunk.Sector.Level.ZPos = lvlZpos;
                    currentChunk.Sector.Level.Sections = new List<LevelSection>();
                    currentRegion.Locations.Add(currentChunk);
                }

                LevelSection currentSection = currentChunk.Sector.Level.Sections.FirstOrDefault(s => s.Y == ySection);
                if (currentSection == null)
                {
                    currentSection = new LevelSection();
                    currentSection.Y = (byte)ySection;
                    currentSection.BlockLight = new byte[2048];
                    //currentSection.Blocks = new byte[4096];
                    //currentSection.Data = new byte[2048];
                    currentSection.SkyLight = new byte[2048];
                    //currentSection.Add = new byte[2048];
                    for (int i = 0; i < 2048; i++)
                    {
                        //currentSection.BlockLight[i] = 255;
                        currentSection.SkyLight[i] = 255;
                    }

                    currentChunk.Sector.Level.Sections.Add(currentSection);
                }

                int blockPos = 256 * yInSection + 16 * zInSection + xInSection;
                //currentSection.Blocks[blockPos] = block.BlockType.Value;
                int maskSemiPosition = blockPos % 2 == 0 ? 0x0F : 0xF0;
                int maskComplementSemiPosition = blockPos % 2 == 0 ? 0xF0 : 0x0F;
                int multiplierSemiPosition = blockPos % 2 == 0 ? 0 : 4;
                //int dataValue = (currentSection.Data[blockPos / 2] & maskComplementSemiPosition) + (block.BlockID_b << multiplierSemiPosition);
                //currentSection.Data[blockPos / 2] = (byte)dataValue;
                if (block.BlockEntity != null)
                {
                    currentChunk.Sector.Level.TileEntities.Add(block.BlockEntity);
                }
            }*/

            return regions;
        }
    }
}
