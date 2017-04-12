﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.Tags
{
    public class TAG_List : NamedTAG
    {
        public sbyte TagId { get; set; }

        public int Size { get; set; }

        public List<BaseTAG> Children { get; set; }

        public override byte TagType { get { return 9; } }


    }
}
