using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business
{
    public class ZLibStreamHelper : IDisposable
    {
        ZlibStream innerStream;

        public Stream BaseStream { get { return innerStream; } }

        public static ZLibStreamHelper ForDecompression(Stream stream)
        {
            return new ZLibStreamHelper(stream, CompressionMode.Decompress);
        }
        public static ZLibStreamHelper ForCompression(Stream stream)
        {
            return new ZLibStreamHelper(stream, CompressionMode.Compress);
        }
        public static ZLibStreamHelper ForCompression(Stream stream, bool leaveOpen)
        {
            return new ZLibStreamHelper(stream, CompressionMode.Compress, leaveOpen);
        }
        private ZLibStreamHelper(Stream stream, CompressionMode mode)
        {
            innerStream = new ZlibStream(stream, mode);
        }
        private ZLibStreamHelper(Stream stream, CompressionMode mode, bool leaveOpen)
        {
            innerStream = new ZlibStream(stream, mode, leaveOpen);
        }
        public void Dispose()
        {
            innerStream.Dispose();
            innerStream = null;
        }
    }
}
