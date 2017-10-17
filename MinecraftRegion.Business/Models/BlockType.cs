using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models
{
    public static class BlockTypes
    {
        public static BlockType Air { get { return new BlockType(0, 0, "minecraft:air"); } }
        public static BlockType Andesite { get { return new BlockType(1, 5, "minecraft:stone"); } }
        public static BlockType Bedrock { get { return new BlockType(7, 0, "minecraft:bedrock"); } }
        public static BlockType CoarseDirt { get { return new BlockType(3, 1, "minecraft:dirt"); } }
        public static BlockType Diorite { get { return new BlockType(1, 3, "minecraft:stone"); } }
        public static BlockType Dirt { get { return new BlockType(3, 0, "minecraft:dirt"); } }
        public static BlockType Granite { get { return new BlockType(1, 1, "minecraft:stone"); } }
        public static BlockType Grass { get { return new BlockType(2, 0, "minecraft:grass"); } }
        public static BlockType Podzol { get { return new BlockType(3, 2, "minecraft:dirt"); } }
        public static BlockType PolishedAndesite { get { return new BlockType(1, 6, "minecraft:stone"); } }
        public static BlockType PolishedDiorite { get { return new BlockType(1, 4, "minecraft:stone"); } }
        public static BlockType PolishedGranite { get { return new BlockType(1, 2, "minecraft:stone"); } }
        public static BlockType RedstoneOre { get { return new BlockType(73, 0, "minecraft:redstone_ore"); } }
        public static BlockType Stone { get { return new BlockType(1, 0, "minecraft:stone"); } }

        static BlockTypes()
        {
            OfficialBlocks = new BlockType[]{
                Air,
                Andesite,
                Bedrock,
                Diorite,
                CoarseDirt,
                Dirt,
                Granite,
                Grass,
                Podzol,
                PolishedAndesite,
                PolishedDiorite,
                PolishedGranite,
                RedstoneOre,
                Stone
            };
            //OfficialBlocks.Add
        }

        private static BlockType[] OfficialBlocks { get; }

        public static BlockType GetBlock(byte value, byte add)
        {
            return OfficialBlocks.First(b => b.Value == value && b.Add == add);
        }
    }

    public struct BlockType
    {
        internal BlockType(byte value, byte add, string name) : this()
        {
            Value = value;
            Add = add;
            Name = name;
        }

        public byte Value { get; }
        public byte Add { get; }
        public String Name { get; }
    }
}
