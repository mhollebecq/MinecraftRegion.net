using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftRegion.Business.Models;
using NBT.Business.Models.Tags;
using MinecraftRegion.Business.Models.BlockEntities;

namespace MinecraftRegion.Business
{
    public class SectorReader
    {
        internal ChunkSector GetSector(BaseTAG baseTAG)
        {
            if (baseTAG == null) throw new ArgumentNullException("baseTag");
            if (!(baseTAG is TAG_Compound)) throw new ArgumentException("base tag of sector can not be other than compound");
            TAG_Compound rootTag = (baseTAG as TAG_Compound);
            ChunkSector sector = new ChunkSector();
            foreach (BaseTAG tag in rootTag.Value)
            {
                //Contain only named tags
                if (tag is INamedTag)
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
        private T CheckTagType<T>(BaseTAG tag) where T : class
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
                            level.Biomes = CheckTagType<TAG_IntArray>(baseTag).Value;
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
                            level.TileEntities = GetBlockEntities(CheckTagType<TAG_List>(baseTag));
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
                //We search id
                BaseTAG idTag = compound.Value.FirstOrDefault(t => (t as INamedTag).Name == "id");
                if (idTag != null)
                {
                    string idTagValue = CheckTagType<TAG_String>(idTag).Value;
                    switch (idTagValue)
                    {
                        case "minecraft:sign":
                            entities.Add(GetSignEntity(compound));
                            break;
                        default:
                            break;
                    }
                }
            }
            return entities;
        }

        private BlockEntity GetSignEntity(TAG_Compound compound)
        {
            SignEntity sign = new SignEntity();
            SetBaseEntityParameters(sign, compound);
            sign.Text1 = CheckTagType<TAG_String>(compound.Value.First(b => (b as INamedTag).Name == "Text1")).Value;
            sign.Text2 = CheckTagType<TAG_String>(compound.Value.First(b => (b as INamedTag).Name == "Text2")).Value;
            sign.Text3 = CheckTagType<TAG_String>(compound.Value.First(b => (b as INamedTag).Name == "Text3")).Value;
            sign.Text4 = CheckTagType<TAG_String>(compound.Value.First(b => (b as INamedTag).Name == "Text4")).Value;
            return sign;
        }

        private void SetBaseEntityParameters(BlockEntity entity, TAG_Compound compound)
        {
            entity.Id = CheckTagType<TAG_String>(compound.Value.First(b => (b as INamedTag).Name == "id")).Value;
            entity.X = CheckTagType<TAG_Int>(compound.Value.First(b => (b as INamedTag).Name == "x")).Value;
            entity.Y = CheckTagType<TAG_Int>(compound.Value.First(b => (b as INamedTag).Name == "y")).Value;
            entity.Z = CheckTagType<TAG_Int>(compound.Value.First(b => (b as INamedTag).Name == "z")).Value;
        }

        private List<LevelSection> GetLevelSections(TAG_List list)
        {
            List<LevelSection> sections = new List<LevelSection>();
            foreach (object elementInList in list.Value)
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
                        case "BlockLight":
                            section.BlockLight = CheckTagType<TAG_ByteArray>(baseTag).Value;
                            break;
                        case "SkyLight":
                            section.SkyLight = CheckTagType<TAG_ByteArray>(baseTag).Value;
                            break;
                        case "Y":
                            section.Y = CheckTagType<TAG_Byte>(baseTag).Value;
                            break;
                        case "Palette":
                            section.Palette = GetPalette(CheckTagType<TAG_List>(baseTag));
                            break;
                        case "BlockStates":
                            section.BlockStates = CheckTagType<TAG_LongArray>(baseTag).Value;
                            break;
                        default:
                            break;
                    }
                }
            }

            return section;
        }

        private List<Palette> GetPalette(TAG_List list)
        {
            if (list == null || list.TagId != 10) throw new ArgumentException("List of palette must have tagId 10");
            List<Palette> palettes = new List<Palette>();

            foreach (TAG_Compound coumpound in list.Value)
            {
                if (coumpound == null) throw new Exception("Palette tag must contains only Compoud");
                Palette palette = new Palette();
                foreach (BaseTAG baseTag in coumpound.Value)
                {
                    //Contain only named tags
                    if (baseTag is INamedTag)
                    {
                        INamedTag namedTag = baseTag as INamedTag;
                        switch (namedTag.Name)
                        {
                            case "Name":
                                palette.Name = CheckTagType<TAG_String>(baseTag).Value;
                                break;
                            case "Properties":
                                palette.Properties = GetPaletteProperties(CheckTagType<TAG_Compound>(baseTag).Value);
                                break;
                        }
                    }
                }
                palettes.Add(palette);
            }

            return palettes;
        }

        private PaletteProperties GetPaletteProperties(List<BaseTAG> values)
        {
            PaletteProperties palette = new PaletteProperties();
            foreach (BaseTAG baseTag in values)
            {
                //Contain only named tags
                if (baseTag is INamedTag)
                {
                    INamedTag namedTag = baseTag as INamedTag;
                    switch (namedTag.Name)
                    {
                        case "Name":
                            palette.Name = CheckTagType<TAG_String>(baseTag).Value;
                            break;
                    }
                }
            }

            return palette;
        }
    }
}
