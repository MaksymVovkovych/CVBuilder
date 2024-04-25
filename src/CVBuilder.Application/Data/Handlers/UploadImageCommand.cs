using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Data.Commands;
using CVBuilder.Application.Data.Responses;
using CVBuilder.Application.Data.Services;
using CVBuilder.Application.Data.Services.Interfaces;
using CVBuilder.Application.Extensions;
using MediatR;
using SkiaSharp;

namespace CVBuilder.Application.Data.Handlers;

public class UploadImageHandler : IRequestHandler<UploadImageCommand, FileResult>
{
    private readonly ImageService _imageService;
    private readonly IMapper _mapper;
    private readonly IIMageCompressService _compressService;

    public UploadImageHandler(ImageService imageService, IMapper mapper, IIMageCompressService compressService)
    {
        _imageService = imageService;
        _mapper = mapper;
        _compressService = compressService;
    }

    public async Task<FileResult> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        if (!_imageService.IsImage(request.File))
            throw ValidationException.Build("Image", "The file is not an image");

        var stream = _compressService.CompressImage(request.File.FileStream, 60);

        await request.File.FileStream.DisposeAsync();
        request.File.FileStream = stream;

        var file = await _imageService.UploadImage(request.File);
        var result = _mapper.Map<FileResult>(file);
        await stream.DisposeAsync();
        return result;
    }
}