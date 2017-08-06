using MinecraftRegion.Business.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business
{
    public class RegionWriter
    {
        public void WriteRegionsToFiles(string basePath, IEnumerable<Region> regions)
        {
            foreach(Region region in regions)
            {
                string fileName = string.Format("r.{0}.{1}.mca", region.X, region.Z);
                using (FileStream fileStream = new FileStream(Path.Combine(basePath, fileName), FileMode.Create))
                {
                    WriteRegionToStream(region, fileStream);
                }
            }

        }
        public void WriteRegionToStream(Region regionToWrite, Stream stream)
        {
            using(BinaryWriter binaryWriter = new BinaryWriter(stream))
            {
                byte[] headerLocations = GetLocationsBytes(regionToWrite.Locations);
                byte[] headerTimestamps = GetTimestampBytes(regionToWrite.Locations);
                binaryWriter.Write(headerLocations);

                binaryWriter.Write(headerTimestamps);



                binaryWriter.Flush();
            }
        }

        private byte[] GetTimestampBytes(IEnumerable<Chunk> locations)
        {
            byte[] headerTimestamp = new byte[4096];

            int iLocation = 0;

            foreach (Chunk chunk in locations)
            {
                headerTimestamp[iLocation] =     (byte)((chunk.Timestamp & 0xFF000000) >> 24);
                headerTimestamp[iLocation + 1] = (byte)((chunk.Timestamp & 0xFF0000) >> 16);
                headerTimestamp[iLocation + 2] = (byte)((chunk.Timestamp & 0xFF00) >> 8);
                headerTimestamp[iLocation + 3] = (byte)(chunk.Timestamp  & 0xFF);

                iLocation+=4;
            }

            return headerTimestamp;
        }

        private byte[] GetLocationsBytes(IEnumerable<Chunk> locations)
        {
            byte[] headerLocations = new byte[4096];

            int iLocation = 0;

            foreach (Chunk chunk in locations)
            {
                headerLocations[iLocation] = (byte)((chunk.Offset & 0xFF0000) >> 16);
                headerLocations[iLocation+1] = (byte)((chunk.Offset & 0xFF00) >> 8);
                headerLocations[iLocation+2] = (byte)(chunk.Offset & 0xFF);
                headerLocations[iLocation+3] = chunk.SectorCount;

                iLocation+=4;
            }

            return headerLocations;
        }
    }
}
