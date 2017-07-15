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
            string regionpath = @"C:\Users\mathi\AppData\Roaming\.minecraft\saves\Prout\region";
            //string oneRegion = @"C:\Users\mathi\AppData\Roaming\.minecraft\saves\Prout\region\r.0.0.mca";
            //string regionpath = @"C:\Games\Minecraft\Serveur\Un\world\region";
            RegionReader reader = new RegionReader();

            foreach (Region region in reader.ReadFolder(regionpath))
            {
                Console.WriteLine("Region - x:{0} - y:{1}", region.X, region.Y);
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