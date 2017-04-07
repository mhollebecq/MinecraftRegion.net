using MinecraftRegion.Business;
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

            reader.Read(regionpath); 
        }
    }
}
