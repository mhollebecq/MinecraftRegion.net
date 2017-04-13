using System;

namespace MinecraftRegion.Business.Models.Tags
{
    internal class TAG_Long : NamedTAG
    {
        public override byte TagType { get { return 4; } }

        public long Value { get; internal set; }
    }
}