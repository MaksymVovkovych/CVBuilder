using System.IO;
using SkiaSharp;

namespace CVBuilder.Application.Data.Services.Interfaces;

public class ImageCompressService:IIMageCompressService
{
    public Stream CompressImage(byte[] bytes, int quality = 100)
    {
        using var original = SKBitmap.Decode(bytes);
        var im = original.Encode(SKEncodedImageFormat.Jpeg, quality).AsStream();
        return im;
    }
    
    public Stream CompressImage(Stream stream, int quality = 100)
    {
        using var original = SKBitmap.Decode(stream);
        var im = original.Encode(SKEncodedImageFormat.Jpeg, quality).AsStream();
        return im;
    }
}