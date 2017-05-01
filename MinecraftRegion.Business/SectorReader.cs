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
                            level.TerrainPopuled = CheckTagType<TAG_Byte>(baseTag).Value;
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
                            level.Sections = CheckTagType<TAG_List>(baseTag).Value;
                            break;
                        case "Entities":
                            level.Entities = CheckTagType<TAG_List>(baseTag).Value;
                            break;
                        case "TileEntities":
                            level.TileEntities = CheckTagType<TAG_List>(baseTag).Value;
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
    }
}
