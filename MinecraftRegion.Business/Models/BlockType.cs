using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models
{
    public static class BlockTypes
    {
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

            public static BlockType GetBlock(byte value, byte add)
            {
                return OfficialBlocks.First(b => b.Value == value /*&& b.Add == add*/);
            }
        }

        public static BlockType Air { get { return new BlockType(0, 0, "minecraft:air"); } }
        public static BlockType Bedrock { get { return new BlockType(7, 0, "minecraft:bedrock"); } }



        static BlockTypes()
        {
            OfficialBlocks = new BlockType[]{
                Air,
                Bedrock
            };
            //OfficialBlocks.Add
        }

        public static BlockType[] OfficialBlocks { get; }
    }
}
