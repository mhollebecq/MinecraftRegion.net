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
        public static BlockType Stone { get { return new BlockType(1, 0, "minecraft:stone"); } }
        public static BlockType Granite { get { return new BlockType(1, 1, "minecraft:stone"); } }
        public static BlockType PolishedGranite { get { return new BlockType(1, 2, "minecraft:stone"); } }
        public static BlockType Diorite { get { return new BlockType(1, 3, "minecraft:stone"); } }
        public static BlockType PolishedDiorite { get { return new BlockType(1, 4, "minecraft:stone"); } }
        public static BlockType Andesite { get { return new BlockType(1, 5, "minecraft:stone"); } }
        public static BlockType PolishedAndesite { get { return new BlockType(1, 6, "minecraft:stone"); } }
        public static BlockType GrassBlock { get { return new BlockType(2, 0, "minecraft:grass"); } }
        public static BlockType Dirt { get { return new BlockType(3, 0, "minecraft:dirt"); } }
        public static BlockType Podzol { get { return new BlockType(3, 2, "minecraft:dirt"); } }
        public static BlockType CoarseDirt { get { return new BlockType(3, 1, "minecraft:dirt"); } }
        public static BlockType Cobblestone { get { return new BlockType(4,0, "minecraft:cobblestone"); } }
        public static BlockType OakWoodPlanks { get { return new BlockType(5,0, "minecraft:planks"); } }
        public static BlockType SpruceWoodPlanks { get { return new BlockType(5,1, "minecraft:planks"); } }
        public static BlockType BirchWoodPlanks { get { return new BlockType(5,2, "minecraft:planks"); } }
        public static BlockType JungleWoodPlanks { get { return new BlockType(5,3, "minecraft:planks"); } }
        public static BlockType AcaciaWoodPlanks { get { return new BlockType(5,4, "minecraft:planks"); } }
        public static BlockType DarkOakWoodPlanks { get { return new BlockType(5,5, "minecraft:planks"); } }
        public static BlockType OakSapling { get { return new BlockType(6,0, "minecraft:sapling"); } }
        public static BlockType SpruceSapling { get { return new BlockType(6, 1, "minecraft:planks"); } }
        public static BlockType BirchSapling { get { return new BlockType(6, 2, "minecraft:planks"); } }
        public static BlockType JungleSapling { get { return new BlockType(6, 3, "minecraft:planks"); } }
        public static BlockType AcaciaSapling { get { return new BlockType(6, 4, "minecraft:planks"); } }
        public static BlockType DarkOakSapling { get { return new BlockType(6, 5, "minecraft:planks"); } }
        public static BlockType Bedrock { get { return new BlockType(7, 0, "minecraft:bedrock"); } }
        public static BlockType Water { get { return new BlockType(8, 0, "minecraft:flowing_water"); } }
        public static BlockType StationaryWater { get { return new BlockType(9, 0, "minecraft:water"); } }
        public static BlockType Lava { get { return new BlockType(10, 0, "minecraft:flowing_lava"); } }
        public static BlockType StationaryLava { get { return new BlockType(11, 0, "minecraft:lava"); } }
        public static BlockType Sand { get { return new BlockType(12, 0, "minecraft:sand"); } }
        public static BlockType RedSand { get { return new BlockType(12, 1, "minecraft:sand"); } }
        public static BlockType Gravel { get { return new BlockType(13, 0, "minecraft:gravel"); } }
        public static BlockType GoldOre { get { return new BlockType(14, 0, "minecraft:gold_ore"); } }
        public static BlockType IronOre { get { return new BlockType(15, 0, "minecraft:iron_ore"); } }
        public static BlockType CoalOre { get { return new BlockType(16, 0, "minecraft:coal_ore"); } }
        public static BlockType OakWood { get { return new BlockType(17, 0, "minecraft:log"); } }
        public static BlockType SpruceWood { get { return new BlockType(17, 1, "minecraft:log"); } }
        public static BlockType BirchWood { get { return new BlockType(17, 2, "minecraft:log"); } }
        public static BlockType JungleWood { get { return new BlockType(17, 3, "minecraft:log"); } }
        public static BlockType Leaves { get { return new BlockType(18, 0, "minecraft:leaves"); } }
        public static BlockType LapisLazuliOre { get { return new BlockType(21, 0, "minecraft:lapis_ore"); } }
        public static BlockType Grass { get { return new BlockType(31, 0, "minecraft:tallgrass"); } }
        public static BlockType Dandelion { get { return new BlockType(37, 0, "minecraft:yellow_flower"); } }
        public static BlockType Poppy { get { return new BlockType(38, 0, "minecraft:red_flower"); } }
        public static BlockType BrownMushroom { get { return new BlockType(39, 0, "minecraft:brown_mushroom"); } }
        public static BlockType RedMushroom { get { return new BlockType(40, 0, "minecraft:red_mushroom"); } }
        public static BlockType MossStone { get { return new BlockType(48, 0, "minecraft:mossy_cobblestone"); } }
        public static BlockType Obsidian { get { return new BlockType(49, 0, "minecraft:obsidian"); } }
        public static BlockType MonsterSpawner { get { return new BlockType(52, 0, "minecraft:mob_spawner"); } }
        public static BlockType Chest { get { return new BlockType(54, 0, "minecraft:chest"); } }
        public static BlockType DiamondOre { get { return new BlockType(56, 0, "minecraft:diamond_ore"); } }
        public static BlockType Sign { get { return new BlockType(63, 0, "minecraft:standing_sign"); } }
        public static BlockType RedstoneOre { get { return new BlockType(73, 0, "minecraft:redstone_ore"); } }
        public static BlockType SnowLayer { get { return new BlockType(78, 0, "minecraft:snow_layer"); } }
        public static BlockType Clay { get { return new BlockType(82, 0, "minecraft:clay"); } }
        public static BlockType SugarCane { get { return new BlockType(83, 0, "minecraft:reeds"); } }
        public static BlockType Pumpkin { get { return new BlockType(86, 0, "minecraft:pumpkin"); } }
        public static BlockType MonsterEgg { get { return new BlockType(97, 0, "minecraft:monster_egg"); } }
        public static BlockType EmeraldOre { get { return new BlockType(129, 0, "minecraft:emerald_ore"); } }
        public static BlockType LargeFlowers { get { return new BlockType(175, 0, "minecraft:double_plant"); } }


        static BlockTypes()
        {
            OfficialBlocks = new BlockType[]{
                AcaciaWoodPlanks,
                AcaciaSapling,
                Air,
                Andesite,
                Bedrock,
                BirchWood,
                BirchWoodPlanks,
                BirchSapling,
                BrownMushroom,
                Chest,
                Clay,
                CoalOre,
                Cobblestone,
                Dandelion,
                DiamondOre,
                Diorite,
                CoarseDirt,
                DarkOakWoodPlanks,
                DarkOakSapling,
                EmeraldOre,
                Dirt,
                GoldOre,
                Granite,
                Grass,
                GrassBlock,
                Gravel,
                IronOre,
                JungleWood,
                JungleWoodPlanks,
                JungleSapling,
                LapisLazuliOre,
                LargeFlowers,
                Lava,
                Leaves,
                MonsterEgg,
                MonsterSpawner,
                MossStone,
                OakWood,
                OakWoodPlanks,
                OakSapling,
                Obsidian,
                Podzol,
                PolishedAndesite,
                PolishedDiorite,
                PolishedGranite,
                Poppy,
                Pumpkin,
                RedMushroom,
                RedSand,
                RedstoneOre,
                Sand,
                Sign,
                SnowLayer,
                SpruceWood,
                SpruceWoodPlanks,
                SpruceSapling,
                StationaryLava,
                StationaryWater,
                SugarCane,
                Stone,
                Water
            };
            //OfficialBlocks.Add
        }

        private static BlockType[] OfficialBlocks { get; }

        public static BlockType GetBlock(byte value, byte add)
        {
            return OfficialBlocks.First(b => b.Value == value /*&& b.Add == add*/);
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
