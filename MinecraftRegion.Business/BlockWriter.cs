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

            foreach (Block block in blocks)
            {
                //int xChunkInWorld = region.X * 32 + chunk.Sector.Level.XPos;
                //  region.X = (xChunkInWorld - chunk.Sector.Level.XPos) / 32;
                //int zChunkInWorld = region.Z * 32 + chunk.Sector.Level.ZPos;

                //int xSection = blockPos % 16;
                //int xWorld = xChunkInWorld * 16 + xSection;
                //int zSection = (blockPos / 16) % 16;
                //int zWorld = zChunkInWorld * 16 + zSection;
                //int yWorld = (blockPos / 256) + 16 * section.Y;

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
                    currentChunk.Sector.Level.Biomes = new byte[256];
                    currentChunk.Sector.Level.Entities = new List<object>();
                    currentChunk.Sector.Level.HeightMap = new int[256];
                    currentChunk.Sector.Level.V = 1;
                    currentChunk.Sector.Level.XPos = lvlXpos;
                    currentChunk.Sector.Level.ZPos = lvlZpos;
                    currentChunk.Sector.Level.Sections = new List<LevelSection>();
                    currentRegion.Locations.Add(currentChunk);
                }

                LevelSection currentSection = currentChunk.Sector.Level.Sections.FirstOrDefault(s => s.Y == ySection);
                if(currentSection == null)
                {
                    currentSection = new LevelSection();
                    currentSection.Y = (byte)ySection;
                    currentSection.BlockLight = new byte[2048];
                    currentSection.Blocks = new byte[4096];
                    currentSection.Data = new byte[2048];
                    currentSection.SkyLight = new byte[2048];

                    currentChunk.Sector.Level.Sections.Add(currentSection);
                }

                int blockPos = 256 * yInSection + 16 * zInSection + xInSection;
                currentSection.Blocks[blockPos] = block.BlockType.Value;
            }

            return regions;
        }
    }
}
