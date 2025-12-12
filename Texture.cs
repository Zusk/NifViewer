using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

public class Texture
{
    public int Handle { get; private set; }

    private Texture(int handle)
    {
        Handle = handle;
    }

    /// <summary>
    /// Tries each path in order and loads the first one that exists.
    /// Decodes using StbImageSharp into RGBA8 and uploads to OpenGL.
    /// </summary>
    public static Texture Load(params string[] possiblePaths)
    {
        string? pathFound = null;

        foreach (var path in possiblePaths)
        {
            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
            {
                pathFound = path;
                break;
            }
        }

        if (pathFound == null)
            throw new FileNotFoundException("Texture file not found.", string.Join(", ", possiblePaths));

        var texData = Path.GetExtension(pathFound).Equals(".dds", StringComparison.OrdinalIgnoreCase)
            ? DdsLoader.Load(pathFound)
            : LoadViaStb(pathFound);

        int handle = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, handle);

        GL.TexImage2D(TextureTarget.Texture2D,
                      level: 0,
                      internalformat: texData.InternalFormat,
                      width: texData.Width,
                      height: texData.Height,
                      border: 0,
                      format: texData.PixelFormat,
                      type: texData.PixelType,
                      pixels: texData.Data);

        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        // Basic filtering and wrap
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        return new Texture(handle);
    }

    public void Bind(TextureUnit unit = TextureUnit.Texture0)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, Handle);
    }

    private sealed class TextureData
    {
        public required byte[] Data { get; init; }
        public required int Width { get; init; }
        public required int Height { get; init; }
        public required PixelInternalFormat InternalFormat { get; init; }
        public required PixelFormat PixelFormat { get; init; }
        public required PixelType PixelType { get; init; }
    }

    private static TextureData LoadViaStb(string path)
    {
        StbImage.stbi_set_flip_vertically_on_load(1);
        using var stream = File.OpenRead(path);
        ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        return new TextureData
        {
            Data = image.Data,
            Width = image.Width,
            Height = image.Height,
            InternalFormat = PixelInternalFormat.Rgba,
            PixelFormat = PixelFormat.Rgba,
            PixelType = PixelType.UnsignedByte
        };
    }

    private static class DdsLoader
    {
        public static TextureData Load(string path)
        {
            using var fs = File.OpenRead(path);
            using var reader = new BinaryReader(fs);

            uint magic = reader.ReadUInt32();
            if (magic != 0x20534444) // "DDS "
                throw new InvalidDataException("Not a DDS file.");

            int headerSize = reader.ReadInt32();
            if (headerSize != 124)
                throw new InvalidDataException("Invalid DDS header size.");

            uint flags = reader.ReadUInt32();
            uint height = reader.ReadUInt32();
            uint width = reader.ReadUInt32();
            reader.ReadUInt32(); // pitch/linear size
            reader.ReadUInt32(); // depth
            reader.ReadUInt32(); // mip map count

            reader.BaseStream.Seek(44, SeekOrigin.Current); // reserved

            int pfSize = reader.ReadInt32();
            if (pfSize != 32)
                throw new InvalidDataException("Invalid DDS pixel format size.");

            uint pfFlags = reader.ReadUInt32();
            uint fourCC = reader.ReadUInt32();
            uint rgbBitCount = reader.ReadUInt32();
            uint rMask = reader.ReadUInt32();
            uint gMask = reader.ReadUInt32();
            uint bMask = reader.ReadUInt32();
            uint aMask = reader.ReadUInt32();

            reader.BaseStream.Seek(20, SeekOrigin.Current); // caps & reserved2

            byte[] remaining = reader.ReadBytes((int)(fs.Length - fs.Position));

            var info = (width: (int)width, height: (int)height, data: remaining,
                        pfFlags, fourCC, rgbBitCount, rMask, gMask, bMask, aMask);

            if ((pfFlags & 0x4u) != 0) // DDPF_FOURCC
            {
                return LoadCompressed(info);
            }

            if ((pfFlags & 0x40u) != 0 && rgbBitCount == 32) // DDPF_RGB | 32 bits
            {
                return LoadUncompressed(info);
            }

            throw new NotSupportedException("Unsupported DDS format.");
        }

        private static TextureData LoadUncompressed((int width, int height, byte[] data, uint pfFlags, uint fourCC, uint rgbBitCount, uint rMask, uint gMask, uint bMask, uint aMask) info)
        {
            byte[] pixels = new byte[info.width * info.height * 4];
            int stride = (int)(info.rgbBitCount / 8);
            for (int y = 0; y < info.height; y++)
            {
                for (int x = 0; x < info.width; x++)
                {
                    int srcIndex = (y * info.width + x) * stride;
                    uint pixel = BitConverter.ToUInt32(info.data, srcIndex);

                    byte r = ExtractChannel(pixel, info.rMask);
                    byte g = ExtractChannel(pixel, info.gMask);
                    byte b = ExtractChannel(pixel, info.bMask);
                    byte a = info.aMask != 0 ? ExtractChannel(pixel, info.aMask) : (byte)255;

                    int dstIndex = (y * info.width + x) * 4;
                    pixels[dstIndex + 0] = r;
                    pixels[dstIndex + 1] = g;
                    pixels[dstIndex + 2] = b;
                    pixels[dstIndex + 3] = a;
                }
            }

            return new TextureData
            {
                Data = pixels,
                Width = info.width,
                Height = info.height,
                InternalFormat = PixelInternalFormat.Rgba,
                PixelFormat = PixelFormat.Rgba,
                PixelType = PixelType.UnsignedByte
            };
        }

        private static byte ExtractChannel(uint value, uint mask)
        {
            if (mask == 0)
                return 0;
            int shift = 0;
            uint temp = mask;
            while ((temp & 1) == 0)
            {
                temp >>= 1;
                shift++;
            }

            uint channel = (value & mask) >> shift;
            int bits = CountBits(mask);
            if (bits >= 8)
                return (byte)(channel >> (bits - 8));
            else
                return (byte)((channel * 255) / ((1 << bits) - 1));
        }

        private static int CountBits(uint value)
        {
            int count = 0;
            while (value != 0)
            {
                count += (int)(value & 1);
                value >>= 1;
            }
            return count;
        }

        private static TextureData LoadCompressed((int width, int height, byte[] data, uint pfFlags, uint fourCC, uint rgbBitCount, uint rMask, uint gMask, uint bMask, uint aMask) info)
        {
            byte[] pixels = info.fourCC switch
            {
                0x31545844u => DecodeDxt1(info.data, info.width, info.height), // DXT1
                0x33545844u => DecodeDxt3(info.data, info.width, info.height), // DXT3
                0x35545844u => DecodeDxt5(info.data, info.width, info.height), // DXT5
                _ => throw new NotSupportedException("Unsupported DDS compression format.")
            };

            return new TextureData
            {
                Data = pixels,
                Width = info.width,
                Height = info.height,
                InternalFormat = PixelInternalFormat.Rgba,
                PixelFormat = PixelFormat.Rgba,
                PixelType = PixelType.UnsignedByte
            };
        }

        private static byte[] DecodeDxt1(byte[] input, int width, int height)
        {
            var output = new byte[width * height * 4];
            int blockCountX = (width + 3) / 4;
            int blockCountY = (height + 3) / 4;
            int src = 0;

            Span<byte> palette = stackalloc byte[16];
            for (int by = 0; by < blockCountY; by++)
            {
                for (int bx = 0; bx < blockCountX; bx++)
                {
                    ushort color0 = BitConverter.ToUInt16(input, src);
                    ushort color1 = BitConverter.ToUInt16(input, src + 2);
                    uint bits = BitConverter.ToUInt32(input, src + 4);
                    src += 8;

                    CreatePalette(color0, color1, palette);

                    for (int py = 0; py < 4; py++)
                    {
                        for (int px = 0; px < 4; px++)
                        {
                            int pixelIndex = py * 4 + px;
                            int code = (int)((bits >> (pixelIndex * 2)) & 0x3);
                            int dstX = bx * 4 + px;
                            int dstY = by * 4 + py;
                            if (dstX >= width || dstY >= height)
                                continue;

                            int dst = (dstY * width + dstX) * 4;
                            int srcPalette = code * 4;
                            output[dst + 0] = palette[srcPalette + 0];
                            output[dst + 1] = palette[srcPalette + 1];
                            output[dst + 2] = palette[srcPalette + 2];
                            output[dst + 3] = palette[srcPalette + 3];
                        }
                    }
                }
            }

            return output;
        }

        private static byte[] DecodeDxt3(byte[] input, int width, int height)
        {
            var output = new byte[width * height * 4];
            int blockCountX = (width + 3) / 4;
            int blockCountY = (height + 3) / 4;
            int src = 0;

            Span<byte> alpha = stackalloc byte[16];
            Span<byte> palette = stackalloc byte[16];
            for (int by = 0; by < blockCountY; by++)
            {
                for (int bx = 0; bx < blockCountX; bx++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        byte byteVal = input[src + i];
                        alpha[i * 2] = (byte)((byteVal & 0x0F) * 17);
                        alpha[i * 2 + 1] = (byte)(((byteVal >> 4) & 0x0F) * 17);
                    }

                    ushort color0 = BitConverter.ToUInt16(input, src + 8);
                    ushort color1 = BitConverter.ToUInt16(input, src + 10);
                    uint bits = BitConverter.ToUInt32(input, src + 12);
                    src += 16;

                    CreatePalette(color0, color1, palette, allowTransparent: false);

                    for (int py = 0; py < 4; py++)
                    {
                        for (int px = 0; px < 4; px++)
                        {
                            int blockIndex = py * 4 + px;
                            int code = (int)((bits >> (blockIndex * 2)) & 0x3);
                            int dstX = bx * 4 + px;
                            int dstY = by * 4 + py;
                            if (dstX >= width || dstY >= height)
                                continue;

                            int dst = (dstY * width + dstX) * 4;
                            int srcPalette = code * 4;
                            output[dst + 0] = palette[srcPalette + 0];
                            output[dst + 1] = palette[srcPalette + 1];
                            output[dst + 2] = palette[srcPalette + 2];
                            output[dst + 3] = alpha[blockIndex];
                        }
                    }
                }
            }

            return output;
        }

        private static byte[] DecodeDxt5(byte[] input, int width, int height)
        {
            var output = new byte[width * height * 4];
            int blockCountX = (width + 3) / 4;
            int blockCountY = (height + 3) / 4;
            int src = 0;

            Span<byte> alphaPalette = stackalloc byte[8];
            Span<byte> alphas = stackalloc byte[16];
            Span<byte> palette = stackalloc byte[16];

            for (int by = 0; by < blockCountY; by++)
            {
                for (int bx = 0; bx < blockCountX; bx++)
                {
                    byte alpha0 = input[src];
                    byte alpha1 = input[src + 1];
                    ulong alphaBits = 0;
                    for (int i = 0; i < 6; i++)
                        alphaBits |= (ulong)input[src + 2 + i] << (8 * i);

                    alphaPalette[0] = alpha0;
                    alphaPalette[1] = alpha1;
                    if (alpha0 > alpha1)
                    {
                        for (int i = 2; i < 8; i++)
                            alphaPalette[i] = (byte)(((8 - i) * alpha0 + (i - 1) * alpha1) / 7);
                    }
                    else
                    {
                        for (int i = 2; i < 6; i++)
                            alphaPalette[i] = (byte)(((6 - i) * alpha0 + (i - 1) * alpha1) / 5);
                        alphaPalette[6] = 0;
                        alphaPalette[7] = 255;
                    }

                    ulong bitsCopy = alphaBits;
                    for (int i = 0; i < 16; i++)
                    {
                        int code = (int)(bitsCopy & 0x7);
                        bitsCopy >>= 3;
                        alphas[i] = alphaPalette[code];
                    }

                    ushort color0 = BitConverter.ToUInt16(input, src + 8);
                    ushort color1 = BitConverter.ToUInt16(input, src + 10);
                    uint colorBits = BitConverter.ToUInt32(input, src + 12);
                    src += 16;

                    CreatePalette(color0, color1, palette, allowTransparent: false);

                    for (int py = 0; py < 4; py++)
                    {
                        for (int px = 0; px < 4; px++)
                        {
                            int blockIndex = py * 4 + px;
                            int code = (int)((colorBits >> (blockIndex * 2)) & 0x3);
                            int dstX = bx * 4 + px;
                            int dstY = by * 4 + py;
                            if (dstX >= width || dstY >= height)
                                continue;

                            int dst = (dstY * width + dstX) * 4;
                            int srcPalette = code * 4;
                            output[dst + 0] = palette[srcPalette + 0];
                            output[dst + 1] = palette[srcPalette + 1];
                            output[dst + 2] = palette[srcPalette + 2];
                            output[dst + 3] = alphas[blockIndex];
                        }
                    }
                }
            }

            return output;
        }

        private static void CreatePalette(ushort color0, ushort color1, Span<byte> palette, bool allowTransparent = true)
        {
            Span<byte> c0 = stackalloc byte[4];
            Span<byte> c1 = stackalloc byte[4];
            DecodeRgb565(color0, c0);
            DecodeRgb565(color1, c1);

            c0[3] = 255;
            c1[3] = 255;

            c0.CopyTo(palette[..4]);
            c1.CopyTo(palette.Slice(4, 4));

            if (color0 > color1 || !allowTransparent)
            {
                palette[8 + 0] = (byte)((2 * c0[0] + c1[0]) / 3);
                palette[8 + 1] = (byte)((2 * c0[1] + c1[1]) / 3);
                palette[8 + 2] = (byte)((2 * c0[2] + c1[2]) / 3);
                palette[8 + 3] = 255;

                palette[12 + 0] = (byte)((c0[0] + 2 * c1[0]) / 3);
                palette[12 + 1] = (byte)((c0[1] + 2 * c1[1]) / 3);
                palette[12 + 2] = (byte)((c0[2] + 2 * c1[2]) / 3);
                palette[12 + 3] = 255;
            }
            else
            {
                palette[8 + 0] = (byte)((c0[0] + c1[0]) / 2);
                palette[8 + 1] = (byte)((c0[1] + c1[1]) / 2);
                palette[8 + 2] = (byte)((c0[2] + c1[2]) / 2);
                palette[8 + 3] = 255;

                palette[12 + 0] = 0;
                palette[12 + 1] = 0;
                palette[12 + 2] = 0;
                palette[12 + 3] = 0;
            }
        }

        private static void DecodeRgb565(ushort value, Span<byte> dest)
        {
            int r = (value >> 11) & 0x1F;
            int g = (value >> 5) & 0x3F;
            int b = value & 0x1F;

            dest[0] = (byte)((r * 255) / 31);
            dest[1] = (byte)((g * 255) / 63);
            dest[2] = (byte)((b * 255) / 31);
            dest[3] = 255;
        }
    }
}
