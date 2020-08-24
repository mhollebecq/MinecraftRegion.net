using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models
{
    public static class BlockTypes
    {
        public static BlockType Air { get { return new BlockType("minecraft:air"); } }
        public static BlockType Stone { get { return new BlockType("minecraft:stone"); } }
        public static BlockType Granite { get { return new BlockType("minecraft:granite"); } }
        public static BlockType PolishedGranite { get { return new BlockType("minecraft:polished_granite"); } }
        public static BlockType Diorite { get { return new BlockType("minecraft:diorite"); } }
        public static BlockType PolishedDiorite { get { return new BlockType("minecraft:polished_diorite"); } }
        public static BlockType Andesite { get { return new BlockType("minecraft:andesite"); } }
        public static BlockType PolishedAndesite { get { return new BlockType("minecraft:polished_andesite"); } }
        public static BlockType GrassBlock { get { return new BlockType("minecraft:grass_block"); } }
        public static BlockType Dirt { get { return new BlockType("minecraft:dirt"); } }
        public static BlockType Podzol { get { return new BlockType("minecraft:podzol"); } }
        public static BlockType CoarseDirt { get { return new BlockType("minecraft:coarse_dirt"); } }
        public static BlockType Cobblestone { get { return new BlockType("minecraft:cobblestone"); } }
        public static BlockType OakWoodPlanks { get { return new BlockType("minecraft:oak_planks"); } }
        public static BlockType SpruceWoodPlanks { get { return new BlockType("minecraft:planks"); } }
        public static BlockType BirchWoodPlanks { get { return new BlockType("minecraft:planks"); } }
        public static BlockType JungleWoodPlanks { get { return new BlockType("minecraft:planks"); } }
        public static BlockType AcaciaWoodPlanks { get { return new BlockType("minecraft:planks"); } }
        public static BlockType DarkOakWoodPlanks { get { return new BlockType("minecraft:planks"); } }
        public static BlockType OakSapling { get { return new BlockType("minecraft:sapling"); } }
        public static BlockType SpruceSapling { get { return new BlockType("minecraft:planks"); } }
        public static BlockType BirchSapling { get { return new BlockType("minecraft:planks"); } }
        public static BlockType JungleSapling { get { return new BlockType("minecraft:planks"); } }
        public static BlockType AcaciaSapling { get { return new BlockType("minecraft:planks"); } }
        public static BlockType DarkOakSapling { get { return new BlockType("minecraft:planks"); } }
        public static BlockType Bedrock { get { return new BlockType("minecraft:bedrock"); } }
        public static BlockType Water { get { return new BlockType("minecraft:flowing_water"); } }
        public static BlockType StationaryWater { get { return new BlockType("minecraft:water"); } }
        public static BlockType Lava { get { return new BlockType("minecraft:flowing_lava"); } }
        public static BlockType StationaryLava { get { return new BlockType("minecraft:lava"); } }
        public static BlockType Sand { get { return new BlockType("minecraft:sand"); } }
        public static BlockType RedSand { get { return new BlockType("minecraft:sand"); } }
        public static BlockType Gravel { get { return new BlockType("minecraft:gravel"); } }
        public static BlockType GoldOre { get { return new BlockType("minecraft:gold_ore"); } }
        public static BlockType IronOre { get { return new BlockType("minecraft:iron_ore"); } }
        public static BlockType CoalOre { get { return new BlockType("minecraft:coal_ore"); } }
        public static BlockType OakWood { get { return new BlockType("minecraft:log"); } }
        public static BlockType SpruceWood { get { return new BlockType("minecraft:log"); } }
        public static BlockType BirchWood { get { return new BlockType("minecraft:log"); } }
        public static BlockType JungleWood { get { return new BlockType("minecraft:log"); } }
        public static BlockType Leaves { get { return new BlockType("minecraft:leaves"); } }
        public static BlockType LapisLazuliOre { get { return new BlockType("minecraft:lapis_ore"); } }
        public static BlockType Sandstone { get { return new BlockType("minecraft:sandstone"); } }
        public static BlockType Grass { get { return new BlockType("minecraft:grass"); } }
        public static BlockType WhiteWool { get { return new BlockType("minecraft:white_wool"); } }
        public static BlockType OrangeWool { get { return new BlockType("minecraft:orange_wool"); } }
        public static BlockType MagentaWool { get { return new BlockType("minecraft:magenta_wool"); } }
        public static BlockType LightBlueWool { get { return new BlockType("minecraft:light_blue_wool"); } }
        public static BlockType YellowWool { get { return new BlockType("minecraft:yellow_wool"); } }
        public static BlockType LimeWool { get { return new BlockType("minecraft:lime_wool"); } }
        public static BlockType PinkWool { get { return new BlockType("minecraft:pink_wool"); } }
        public static BlockType GrayWool { get { return new BlockType("minecraft:gray_wool"); } }
        public static BlockType LightGrayWool { get { return new BlockType("minecraft:light_gray_wool"); } }
        public static BlockType CyanWool { get { return new BlockType("minecraft:cyan_wool"); } }
        public static BlockType PurpleWool { get { return new BlockType("minecraft:purple_wool"); } }
        public static BlockType BlueWool { get { return new BlockType("minecraft:blue_wool"); } }
        public static BlockType BrownWool { get { return new BlockType("minecraft:brown_wool"); } }
        public static BlockType GreenWool { get { return new BlockType("minecraft:green_wool"); } }
        public static BlockType RedWool { get { return new BlockType("minecraft:red_wool"); } }
        public static BlockType BlackWool { get { return new BlockType("minecraft:black_wool"); } }
        public static BlockType Dandelion { get { return new BlockType("minecraft:yellow_flower"); } }
        public static BlockType Poppy { get { return new BlockType("minecraft:red_flower"); } }
        public static BlockType BrownMushroom { get { return new BlockType("minecraft:brown_mushroom"); } }
        public static BlockType RedMushroom { get { return new BlockType("minecraft:red_mushroom"); } }
        public static BlockType MossStone { get { return new BlockType("minecraft:mossy_cobblestone"); } }
        public static BlockType Obsidian { get { return new BlockType("minecraft:obsidian"); } }
        public static BlockType MonsterSpawner { get { return new BlockType("minecraft:mob_spawner"); } }
        public static BlockType Chest { get { return new BlockType("minecraft:chest"); } }
        public static BlockType DiamondOre { get { return new BlockType("minecraft:diamond_ore"); } }
        public static BlockType Sign { get { return new BlockType("minecraft:standing_sign"); } }
        public static BlockType Rail { get { return new BlockType("minecraft:rail"); } }
        public static BlockType RedstoneOre { get { return new BlockType("minecraft:redstone_ore"); } }
        public static BlockType SnowLayer { get { return new BlockType("minecraft:snow_layer"); } }
        public static BlockType Clay { get { return new BlockType("minecraft:clay"); } }
        public static BlockType SugarCane { get { return new BlockType("minecraft:reeds"); } }
        public static BlockType Pumpkin { get { return new BlockType("minecraft:pumpkin"); } }
        public static BlockType MonsterEgg { get { return new BlockType("minecraft:monster_egg"); } }
        public static BlockType EmeraldOre { get { return new BlockType("minecraft:emerald_ore"); } }
        public static BlockType LargeFlowers { get { return new BlockType("minecraft:double_plant"); } }


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
                BlackWool,
                BlueWool,
                BrownMushroom,
                BrownWool,
                Chest,
                Clay,
                CoalOre,
                Cobblestone,
                CyanWool,
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
                GrayWool,
                GreenWool,
                IronOre,
                JungleWood,
                JungleWoodPlanks,
                JungleSapling,
                LapisLazuliOre,
                LargeFlowers,
                Lava,
                Leaves,
                LightBlueWool,
                LightGrayWool,
                LimeWool,
                MagentaWool,
                MonsterEgg,
                MonsterSpawner,
                MossStone,
                OakWood,
                OakWoodPlanks,
                OakSapling,
                Obsidian,
                OrangeWool,
                PinkWool,
                Podzol,
                PolishedAndesite,
                PolishedDiorite,
                PolishedGranite,
                Poppy,
                Pumpkin,
                PurpleWool,
                Rail,
                RedMushroom,
                RedSand,
                RedstoneOre,
                RedWool,
                Sand,
                Sandstone,
                Sign,
                SnowLayer,
                SpruceWood,
                SpruceWoodPlanks,
                SpruceSapling,
                StationaryLava,
                StationaryWater,
                SugarCane,
                Stone,
                Water,
                WhiteWool,
                YellowWool
            };
            //OfficialBlocks.Add
        }

        private static BlockType[] OfficialBlocks { get; }

        public static BlockType GetBlock(string name)
        {
            return OfficialBlocks.First(b => b.Name == name);
        }
    }

    public struct BlockType
    {
        internal BlockType(string name) : this()
        {
            Name = name;
        }
        public String Name { get; }
    }
}
