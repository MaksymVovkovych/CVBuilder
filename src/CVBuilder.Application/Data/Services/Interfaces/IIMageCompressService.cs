using System.IO;
using SkiaSharp;

namespace CVBuilder.Application.Data.Services.Interfaces;

public interface IIMageCompressService
{
    Stream CompressImage(byte[] bytes, int quality = 100);

    Stream CompressImage(Stream stream, int quality = 100);

}