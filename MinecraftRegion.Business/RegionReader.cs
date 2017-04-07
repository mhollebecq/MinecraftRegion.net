using MinecraftRegion.Business.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business
{
    public class RegionReader
    {
        public void Read(string path)
        {
            foreach (var file in System.IO.Directory.GetFiles(path, "r.*.*.mca"))
            {
                ReadOne(file);
            }
        }

        private void ReadOne(string file)
        {
            using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(file)))
            {
                byte[] headerLocations = new byte[4096];
                int read = binaryReader.Read(headerLocations, 0, 4096);
                var locations = ReadLocations(headerLocations);
            }
        }

        private IEnumerable<ChunkLocation> ReadLocations(byte[] headerLocations)
        {
            //Location information for a chunk consists of four bytes split into two fields: the first three bytes 
            //are a (big - endian) offset in 4KiB sectors from the start of the file, 
            //and a remaining byte which gives the length of the chunk (also in 4KiB sectors, rounded up).Chunks will always be less than 1MiB in size.
            //If a chunk isn't present in the region file (e.g. because it hasn't been generated or migrated yet), both fields will be zero.

            //byte          0 1 2   3
            //description   offset  sector count
            //A chunk with an offset of 2 will begin right after the timestamps table.
            List<ChunkLocation> locations = new List<ChunkLocation>();
            for (int iLocation = 0; iLocation < 4096; iLocation += 4)
            {
                byte[] location = new byte[4] { headerLocations[iLocation], headerLocations[iLocation + 1], headerLocations[iLocation + 2], headerLocations[iLocation + 3] };
                locations.Add( new ChunkLocation()
                {
                    Offset = (location[0] << 16) + (location[1] << 8) + location[2],
                    SectorCount = location[3]
                });
            }

            return locations;
        }
    }
}
