using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Data.Commands;
using CVBuilder.Application.Data.Responses;
using CVBuilder.Application.Data.Services;
using MediatR;

namespace CVBuilder.Application.Data.Handlers;

public class UploadFileHandler : IRequestHandler<UploadFileCommand, FileResult>
{
    private readonly FileService _fileService;
    private readonly IMapper _mapper;

    public UploadFileHandler(FileService fileService, IMapper mapper)
    {
        _fileService = fileService;
        _mapper = mapper;
    }


    public async Task<FileResult> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var file = await _fileService.CreateFileAsync(request.File);
        var result = _mapper.Map<FileResult>(file);
        return result;
    }
    
    
}