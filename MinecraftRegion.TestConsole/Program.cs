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
            string writeRegionPath = @"C:\Users\mathi\AppData\Roaming\.minecraft\saves\TestWorldMC\region";
            string regionpath = args[0];
            RegionReader reader = new RegionReader();

            var readRegions = reader.ReadFolder(regionpath);
            foreach (Region region in readRegions)
            {
                Console.WriteLine("Region - x:{0} - y:{1}", region.X, region.Z);
                foreach (Chunk location in region.Locations)
                {
                    Console.Write("{0}:{1}-", location.Offset, location.SectorCount);
                    //foreach (ChunkSector sector in location.Sectors)
                    //{
                    //    Console.WriteLine("RegionFile : {2}. Sector - x:{0} - z:{1} : ",
                    //        sector.Level.XPos, 
                    //        sector.Level.ZPos,
                    //        region.Path);
                    //}
                }
                    Console.WriteLine();
            }

            RegionWriter writer = new RegionWriter();
            writer.WriteRegionsToFiles(writeRegionPath, readRegions);

            var secondReadRegions = reader.ReadFolder(writeRegionPath);
            foreach (Region region in secondReadRegions)
            {
                Console.WriteLine("Region - x:{0} - y:{1}", region.X, region.Z);
                foreach (Chunk location in region.Locations)
                {
                    foreach (ChunkSector sector in location.Sectors)
                    {
                        Console.WriteLine("RegionFile : {2}. Sector - x:{0} - z:{1} : ",
                            sector.Level.XPos,
                            sector.Level.ZPos,
                            region.Path);
                    }
                }
            }

            Console.ReadKey();
        }
    }
}