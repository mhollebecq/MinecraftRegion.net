﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.Tags
{
    public class TAG_Int : NamedTAG
    {
        public override byte TagType { get { return 3; } }

        public int Value { get; set; }
    }
}