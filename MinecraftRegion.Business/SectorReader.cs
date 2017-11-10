using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftRegion.Business.Models;
using NBT.Business.Models.Tags;

namespace MinecraftRegion.Business
{
    public class SectorReader
    {
        internal ChunkSector GetSector(BaseTAG baseTAG)
        {
            if (baseTAG == null) throw new ArgumentNullException("baseTag");
            if(!(baseTAG is TAG_Compound)) throw new ArgumentException("base tag of sector can not be other than compound");
            TAG_Compound rootTag = (baseTAG as TAG_Compound);
            ChunkSector sector = new ChunkSector();
            foreach(BaseTAG tag in rootTag.Value)
            {
                //Contain only named tags
                if(tag is INamedTag)
                {
                    INamedTag namedTag = tag as INamedTag;
                    switch (namedTag.Name)
                    {
                        case "Level":
                            sector.Level = GetLevel(CheckTagType<TAG_Compound>(tag));
                            break;
                        case "DataVersion":
                            sector.DataVersion = CheckTagType<TAG_Int>(tag).Value;
                            break;
                        default:
                            break;
                    }
                }
            }

            return sector;
        }

        /// <summary>
        /// Will throw if argument null or bad type
        /// </summary>
        /// <typeparam name="T">ExpectedType</typeparam>
        /// <param name="tag">to convert type</param>
        /// <returns>return expected type or throw exception. Should never return null</returns>
        private T CheckTagType<T>(BaseTAG tag) where T:class
        {
            if (tag == null) throw new ArgumentNullException("tag");
            if (!(tag is T)) throw new ArgumentException(string.Format("Tag is not of type {0} but {1}", typeof(T), tag.GetType()));
            return tag as T;
        }

        private Level GetLevel(TAG_Compound tAG_Compound)
        {
            Level level = new Level();
            foreach (BaseTAG baseTag in tAG_Compound.Value)
            {
                //Contain only named tags
                if (baseTag is INamedTag)
                {
                    INamedTag namedTag = baseTag as INamedTag;
                    switch (namedTag.Name)
                    {
                        case "xPos":
                            level.XPos = CheckTagType<TAG_Int>(baseTag).Value;
                            break;
                        case "zPos":
                            level.ZPos = CheckTagType<TAG_Int>(baseTag).Value;
                            break;
                        case "LastUpdate":
                            level.LastUpdate = CheckTagType<TAG_Long>(baseTag).Value;
                            break;
                        case "LightPopulated":
                            level.LightPopulated = CheckTagType<TAG_Byte>(baseTag).Value;
                            break;
                        case "TerrainPopuled":
                            level.TerrainPopulated = CheckTagType<TAG_Byte>(baseTag).Value;
                            break;
                        case "V":
                            level.V = CheckTagType<TAG_Byte>(baseTag).Value;
                            break;
                        case "InhabitedTime":
                            level.InhabitedTime = CheckTagType<TAG_Long>(baseTag).Value;
                            break;
                        case "Biomes":
                            level.Biomes = CheckTagType<TAG_ByteArray>(baseTag).Value;
                            break;
                        case "HeightMap":
                            level.HeightMap = CheckTagType<TAG_IntArray>(baseTag).Value;
                            break;
                        case "Sections":
                            level.Sections = GetLevelSections(CheckTagType<TAG_List>(baseTag));
                            break;
                        case "Entities":
                            level.Entities = CheckTagType<TAG_List>(baseTag).Value;
                            break;
                        case "TileEntities":
                            level.TileEntities = GetBlockEntities( CheckTagType<TAG_List>(baseTag));
                            break;
                        case "TileTicks":
                            level.TileTicks = CheckTagType<TAG_List>(baseTag).Value;
                            break;
                        default:
                            break;
                    }
                }
            }
            return level;
        }

        private List<BlockEntity> GetBlockEntities(TAG_List value)
        {
            List<BlockEntity> entities = new List<BlockEntity>();
            foreach (TAG_Compound compound in value.Value)
            {
                foreach (BaseTAG baseTag in compound.Value)
                {
                    BlockEntity entity = new BlockEntity();
                    INamedTag namedTag = baseTag as INamedTag;
                    switch (namedTag.Name)
                    {
                        case "id":
                            entity.Id = CheckTagType<TAG_String>(baseTag).Value;
                            break;
                    }
                }
            }
            return entities;
        }

        private List<LevelSection> GetLevelSections(TAG_List list)
        {
            List<LevelSection> sections = new List<LevelSection>();
            foreach(object elementInList in list.Value)
            {
                TAG_Compound coumpound = elementInList as TAG_Compound;
                if (coumpound == null) throw new ArgumentException("Section must be stored into TAG_Coumpound");

                sections.Add(GetLevelSection(coumpound));
            }
            return sections;
        }

        private LevelSection GetLevelSection(TAG_Compound coumpound)
        {
            LevelSection section = new LevelSection();

            foreach (BaseTAG baseTag in coumpound.Value)
            {
                //Contain only named tags
                if (baseTag is INamedTag)
                {
                    INamedTag namedTag = baseTag as INamedTag;
                    switch (namedTag.Name)
                    {
                        case "Add":
                            section.Add = CheckTagType<TAG_ByteArray>(baseTag).Value;
                            break;
                        case "BlockLight":
                            section.BlockLight = CheckTagType<TAG_ByteArray>(baseTag).Value;
                            break;
                        case "Blocks":
                            section.Blocks = CheckTagType<TAG_ByteArray>(baseTag).Value;
                            break;
                        case "Data":
                            section.Data = CheckTagType<TAG_ByteArray>(baseTag).Value;
                            break;
                        case "SkyLight":
                            section.SkyLight = CheckTagType<TAG_ByteArray>(baseTag).Value;
                            break;
                        case "Y":
                            section.Y = CheckTagType<TAG_Byte>(baseTag).Value;
                            break;
                        default:
                            break;
                    }
                }
            }

            return section;
        }
    }
}
