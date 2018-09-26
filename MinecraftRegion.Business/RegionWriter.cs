using MinecraftRegion.Business.Models;
using MinecraftRegion.Business.Models.BlockEntities;
using NBT.Business;
using NBT.Business.Models.Tags;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business
{
    public class RegionWriter
    {
        public void WriteRegionsToFiles(string basePath, IEnumerable<Region> regions)
        {
            foreach (Region region in regions)
            {
                string fileName = string.Format("r.{0}.{1}.mca", region.X, region.Z);
                using (FileStream fileStream = new FileStream(Path.Combine(basePath, fileName), FileMode.Create))
                {
                    WriteRegionToStream(region, fileStream);
                }
            }

        }
        public void WriteRegionToStream(Region regionToWrite, Stream stream)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(stream))
            {
                byte[] headerLocations = new byte[4096];
                byte[] headerTimestamps = new byte[4096];

                NBTWriter nbtWriter = new NBTWriter();
                int total4096words = 2;
                List<byte[]> sectorsList = new List<byte[]>();
                System.Diagnostics.Debug.WriteLine("Location count: " + regionToWrite.Locations.Count());
                foreach (Chunk location in regionToWrite.Locations)
                {
                    System.Diagnostics.Debug.WriteLine("Chunk X: " + location.Sector.Level.XPos+ " Z: "+ location.Sector.Level.ZPos);
                    TAG_Compound chunkTag = GetChunkTag(location.Sector);

                    byte[] chunkBytes = nbtWriter.GetBytes(chunkTag);
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        using (ZLibStreamHelper zLibStream = ZLibStreamHelper.ForCompression(mStream, true))
                        {
                            zLibStream.BaseStream.Write(chunkBytes, 0, chunkBytes.Length);
                            //zLibStream.BaseStream.Flush();
                        }
                        int compressedDataLengthPlusHeader = (int)mStream.Length + 5;
                        int sectorCount = (int)(compressedDataLengthPlusHeader / 4046) +
                            (compressedDataLengthPlusHeader % 4096 == 0 ? 0 : 1);

                        int iLocationAndTimestamp = 4 * ((location.Sector.Level.XPos & 31) + (location.Sector.Level.ZPos & 31) * 32);
                        headerLocations[iLocationAndTimestamp] = (byte)((total4096words & 0xFF0000) >> 16);
                        headerLocations[iLocationAndTimestamp + 1] = (byte)((total4096words & 0xFF00) >> 8);
                        headerLocations[iLocationAndTimestamp + 2] = (byte)((total4096words & 0xFF) >> 0);
                        headerLocations[iLocationAndTimestamp + 3] = (byte)(sectorCount);

                        headerTimestamps[iLocationAndTimestamp] = (byte)((location.Timestamp & 0xFF000000) >> 24);
                        headerTimestamps[iLocationAndTimestamp + 1] = (byte)((location.Timestamp & 0xFF0000) >> 16);
                        headerTimestamps[iLocationAndTimestamp + 2] = (byte)((location.Timestamp & 0xFF00) >> 8);
                        headerTimestamps[iLocationAndTimestamp + 3] = (byte)(location.Timestamp & 0xFF);

                        mStream.Position = 0;
                        byte[] sectorBytes = new byte[4096 * sectorCount];
                        sectorBytes[0] = (byte)((mStream.Length & 0xFF000000) >> 24);
                        sectorBytes[1] = (byte)((mStream.Length & 0xFF0000) >> 16);
                        sectorBytes[2] = (byte)((mStream.Length & 0xFF00) >> 8);
                        sectorBytes[3] = (byte)((mStream.Length & 0xFF) >> 0);
                        sectorBytes[4] = 2;
                        mStream.Read(sectorBytes,
                            5,
                            (int)mStream.Length);
                        sectorsList.Add(sectorBytes);
                        total4096words += sectorCount;
                    }
                }
                binaryWriter.Write(headerLocations);

                binaryWriter.Write(headerTimestamps);

                foreach (byte[] sectorBytes in sectorsList)
                {
                    binaryWriter.Write(sectorBytes);
                }

                binaryWriter.Flush();
            }
        }

        private static TAG_Compound GetChunkTag(ChunkSector sector)
        {
            TAG_Compound chunkTag = new TAG_Compound();
            chunkTag.Value.Add(new TAG_Int()
            {
                Name = "DataVersion",
                Value = sector.DataVersion
            });
            TAG_Compound levelTag = GetLevelTag(sector.Level);
            chunkTag.Value.Add(levelTag);
            return chunkTag;
        }

        private static TAG_Compound GetLevelTag(Level level)
        {
            TAG_Compound levelTag = new TAG_Compound();
            levelTag.Name = "Level";
            levelTag.Value.Add(new TAG_Int()
            {
                Name = "xPos",
                Value = level.XPos
            });
            levelTag.Value.Add(new TAG_Int()
            {
                Name = "zPos",
                Value = level.ZPos
            });

            levelTag.Value.Add(new TAG_Long()
            {
                Name = "LastUpdate",
                Value = level.LastUpdate
            });
            levelTag.Value.Add(new TAG_Byte()
            {
                Name = "LightPopulated",
                Value = level.LightPopulated
            });

            levelTag.Value.Add(new TAG_Byte()
            {
                Name = "TerrainPopulated",
                Value = level.TerrainPopulated
            });
            levelTag.Value.Add(new TAG_Byte()
            {
                Name = "V",
                Value = level.V
            });

            levelTag.Value.Add(new TAG_Long()
            {
                Name = "InhabitedTime",
                Value = level.InhabitedTime
            });
            levelTag.Value.Add(new TAG_IntArray()
            {
                Name = "Biomes",
                Value = level.Biomes
            });

            levelTag.Value.Add(new TAG_IntArray()
            {
                Name = "HeightMap",
                Value = level.HeightMap
            });
            //if (level.Entities != null && level.Entities.Any())
            //    levelTag.Value.Add(new TAG_List()
            //    {
            //        TagId = (sbyte)TagType.Compound,
            //        Name = "Entities",
            //        Value = GetEntitiesTags(level.Entities)
            //    });
            //else
            levelTag.Value.Add(new TAG_List()
            {
                TagId = (sbyte)TagType.End,
                Name = "Entities",
                Value = new List<object>()
            });

            if (level.TileEntities != null && level.TileEntities.Any())
                levelTag.Value.Add(new TAG_List()
                {
                    TagId = (sbyte)TagType.Compound,
                    Name = "TileEntities",
                    Value = GetTileEntitiesTags(level.TileEntities)
                });
            else
                levelTag.Value.Add(new TAG_List()
                {
                    TagId = (sbyte)TagType.End,
                    Name = "TileEntities",
                    Value = new List<object>()
                });

            if (level.TileTicks != null)
                levelTag.Value.Add(new TAG_List()
                {
                    TagId = (sbyte)TagType.Compound,
                    Name = "TileTicks",
                    Value = level.TileTicks
                });

            TAG_List leveSectionsTag = GetLevelSectionsTag(level.Sections);
            levelTag.Value.Add(leveSectionsTag);

            return levelTag;
        }

        private static TAG_List GetLevelSectionsTag(List<LevelSection> sections)
        {
            if (sections == null) throw new ArgumentNullException("Sections must be set");
            TAG_List sectionsTag = new TAG_List();
            sectionsTag.Name = "Sections";
            sectionsTag.TagId = (sbyte)TagType.Compound;
            foreach (LevelSection section in sections)
            {
                sectionsTag.Value.Add(GetLevelSectionTag(section));
            }
            sectionsTag.Size = sectionsTag.Value.Count;
            return sectionsTag;
        }

        private static TAG_Compound GetLevelSectionTag(LevelSection section)
        {
            TAG_Compound sectionTag = new TAG_Compound();

            sectionTag.Value.Add(new TAG_ByteArray()
            {
                Name = "BlockLight",
                Value = section.BlockLight
            });
            sectionTag.Value.Add(new TAG_ByteArray()
            {
                Name = "SkyLight",
                Value = section.SkyLight
            });
            sectionTag.Value.Add(new TAG_Byte()
            {
                Name = "Y",
                Value = section.Y
            });
            sectionTag.Value.Add(GetPalettes(section.Palette));
            sectionTag.Value.Add(new TAG_LongArray()
            {
                Name = "BlockStates",
                Value = section.BlockStates
            });

            return sectionTag;
        }

        private static TAG_List GetPalettes(List<Palette> palette)
        {
            TAG_List palettesTag = new TAG_List();
            palettesTag.Name = "Palette";
            palettesTag.TagId = (sbyte)TagType.Compound;

            foreach(Palette paletteObj in palette)
            {
                palettesTag.Value.Add(GetPalette(paletteObj));
            }

            return palettesTag;
        }

        private static TAG_Compound GetPalette(Palette paletteObj)
        {
            TAG_Compound paletteTag = new TAG_Compound();

            paletteTag.Value.Add(new TAG_String()
            {
                Name = "Name",
                Value = paletteObj.Name
            });

            if (paletteObj.Properties != null)
            {

            }
            return paletteTag;
        }

        private static List<object> GetEntitiesTags(List<object> entities)
        {
            throw new NotImplementedException();
        }

        private static List<object> GetTileEntitiesTags(List<BlockEntity> tileEntities)
        {
            List<object> compoundList = new List<object>();

            foreach (var item in tileEntities)
            {
                if (item is SignEntity)
                    compoundList.Add(GetSignEntity(item as SignEntity));
            }

            return compoundList;
        }

        private static TAG_Compound GetSignEntity(SignEntity item)
        {
            TAG_Compound compound = new TAG_Compound();
            compound.Value = new List<BaseTAG>();
            compound.Value.Add(new TAG_String() { Name = "id", Value = item.Id });
            compound.Value.Add(new TAG_Int() { Name = "x", Value = item.X });
            compound.Value.Add(new TAG_Int() { Name = "y", Value = item.Y });
            compound.Value.Add(new TAG_Int() { Name = "z", Value = item.Z });
            compound.Value.Add(new TAG_String() { Name = "Text1", Value = item.Text1??"" });
            compound.Value.Add(new TAG_String() { Name = "Text2", Value = item.Text2??"" });
            compound.Value.Add(new TAG_String() { Name = "Text3", Value = item.Text3??"" });
            compound.Value.Add(new TAG_String() { Name = "Text4", Value = item.Text4??"" });
            return compound;
        }
    }
}
