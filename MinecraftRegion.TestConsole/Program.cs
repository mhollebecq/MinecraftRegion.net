using MinecraftRegion.Business;
using MinecraftRegion.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string regionpath = @"C:\Games\Minecraft\Serveur\Un\world\region";
            RegionReader reader = new RegionReader();

            foreach (Region region in reader.Read(regionpath))
            {
                foreach(ChunkLocation location in region.Locations)
                {

                }
            }
        }
    }
}