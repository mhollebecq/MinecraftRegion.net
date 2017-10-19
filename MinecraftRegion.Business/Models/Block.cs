﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MinecraftRegion.Business.Models.BlockTypes;

namespace MinecraftRegion.Business.Models
{
    public class Block
    {
        public int XSection { get; set; }
        public int XWorld { get; set; } 
        public int ZSection { get; set; } 
        public int ZWorld { get; set; } 
        public int YWorld { get; set; } 
        public byte BlockID_a { get; set; } 
        public byte BlockID_b { get; set; } 
        public short BlockID { get; set; }
        public BlockType BlockType { get; set; }
    }
}
