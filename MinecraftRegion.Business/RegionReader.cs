﻿using MinecraftRegion.Business.Models;
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
                byte[] headerTimestamps = new byte[4096];
                int read = binaryReader.Read(headerLocations, 0, 4096);
                read = binaryReader.Read(headerTimestamps, 0, 4096);
                var locations = ReadLocations(headerLocations);
                var timeStamps = ReadTimeStamps(headerTimestamps);
                for (int iLocation = 21; iLocation < locations.Count; iLocation++)
                {
                    int timestamp = timeStamps[iLocation];
                    ChunkLocation location = locations[iLocation];
                    binaryReader.BaseStream.Position = location.Offset * 4096;
                    byte[] headerChunk = new byte[5];
                    binaryReader.Read(headerChunk, 0, 5);
                    for (int iSector = 0; iSector < location.SectorCount; iSector++)
                    {
                        //byte          0 1 2 3             4                   5...
                        //description   length(in bytes)    compression type    compressed data (length - 1 bytes)
                        int length = (headerChunk[0] << 24) + (headerChunk[1] << 16) + (headerChunk[2] << 8) + headerChunk[3];
                        byte compresionType = headerChunk[4];

                        switch (compresionType)
                        {
                            case 2:
                                byte[] compressedChunk = new byte[4091];
                                binaryReader.Read(compressedChunk, 0, 4091);
                                using (MemoryStream mStream = new MemoryStream(compressedChunk))
                                {
                                    using (Ionic.Zlib.ZlibStream zLibStream = new Ionic.Zlib.ZlibStream(mStream, Ionic.Zlib.CompressionMode.Decompress))
                                    {
                                        //byte[] decompressedChunk = new byte[zLibStream.BufferSize];
                                        byte[] tagType = new byte[1];
                                        zLibStream.Read(tagType, 0, 1);
                                        switch (tagType[0])
                                        {
                                            case 0:
                                                break;
                                            case 1:
                                                break;
                                            case 2:
                                                break;
                                            case 3:
                                                break;
                                            case 4:
                                                break;
                                            case 5:
                                                break;
                                            case 6:
                                                break;
                                            case 7:
                                                break;
                                            case 8:
                                                break;
                                            case 9:
                                                break;
                                            case 10:
                                                break;
                                            case 11:
                                                break;
                                            case 12:
                                                break;
                                            default:
                                                break;
                                        }
                                        //zLibStream.ReadAsync (decompressedChunk, 0, zLibStream.BufferSize);
                                    }
                                }
                                break;
                            default:
                                throw new NotImplementedException("Only compression ZLib is implemented");
                        }
                    }
                }
            }
        }

        private List<int> ReadTimeStamps(byte[] headerTimestamps)
        {
            List<int> timestamps = new List<int>();
            for (int iTimeStamp = 0; iTimeStamp < 4096; iTimeStamp += 4)
            {
                timestamps.Add((headerTimestamps[iTimeStamp + 0] << 24) + (headerTimestamps[iTimeStamp + 1] << 16) + (headerTimestamps[iTimeStamp + 2] << 8) + (headerTimestamps[iTimeStamp + 3]));
            }
            return timestamps;
        }

        private List<ChunkLocation> ReadLocations(byte[] headerLocations)
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
                int offset = (location[0] << 16) + (location[1] << 8) + location[2];
                byte sectorCount = location[3];
                if (offset != 0 && sectorCount != 0)
                    locations.Add(new ChunkLocation()
                    {
                        Offset = offset,
                        SectorCount = sectorCount
                    });
            }

            return locations;
        }
    }
}
