﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business.Models.Tags
{
    class TAG_IntArray : NamedTAG
    {
        public override byte TagType { get { return 11; } }

        public int[] Values { get; set; }
    }
}