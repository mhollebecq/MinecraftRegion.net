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
            string oneRegion = @"C:\Users\mathi\AppData\Roaming\.minecraft\saves\Prout\region\r.-1.0.mca";
            string writeRegionPath = @"C:\Users\mathi\AppData\Roaming\.minecraft\saves\TestWorldMC\region";
            string regionpath = args[0];
            RegionReader reader = new RegionReader();

            var readRegions = reader.ReadFolder(regionpath);
            foreach (Region region in readRegions)
            {
                Console.WriteLine("Region - x:{0} - y:{1}", region.X, region.Z);
                //foreach (Chunk location in region.Locations)
                //{
                //    Console.WriteLine("{0}:{1}-", location.Offset, location.SectorCount);

                //    ChunkSector sector = location.Sector;
                //    Console.WriteLine("Sector - x:{0} - z:{1} - Location : {2} - Offset : {3}",
                //        sector.Level.XPos,
                //        sector.Level.ZPos,
                //        4 * ((sector.Level.XPos % 32) + (sector.Level.ZPos % 32) * 32),
                //        4 * ((sector.Level.XPos & 31) + (sector.Level.ZPos & 31) * 32));
                //    Console.WriteLine();

                //}
                Console.WriteLine();
            }

            RegionWriter writer = new RegionWriter();
            writer.WriteRegionsToFiles(writeRegionPath, readRegions);

            var secondReadRegions = reader.ReadFolder(writeRegionPath);
            foreach (Region region in secondReadRegions)
            {
                BlockReader blockReader = new BlockReader();
                blockReader.ReadBlocks(region);
                Console.WriteLine("Region - x:{0} - y:{1}", region.X, region.Z);
                foreach (Chunk location in region.Locations)
                {
                    ChunkSector sector = location.Sector;
                    Console.WriteLine("RegionFile : {2}. Sector - x:{0} - z:{1} : ",
                        sector.Level.XPos,
                        sector.Level.ZPos,
                        region.Path);
                }
            }

            Console.ReadKey();
        }
    }
}