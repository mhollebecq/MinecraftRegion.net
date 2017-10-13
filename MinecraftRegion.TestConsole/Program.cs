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
            string writeRegionPath = args[1];
            string regionpath = args[0];
            RegionReader reader = new RegionReader();

            var readRegions = reader.ReadFolder(regionpath);


            //RegionWriter writer = new RegionWriter();
            //writer.WriteRegionsToFiles(writeRegionPath, readRegions);

            BlockReader blockReader = new BlockReader();
            foreach (Region region in readRegions)
            {
                blockReader.ReadBlocks(region);
                
            }
            Console.ReadKey();
        }
    }
}