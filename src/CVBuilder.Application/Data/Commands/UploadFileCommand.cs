using CVBuilder.Application.Data.Responses;
using MediatR;

namespace CVBuilder.Application.Data.Commands;

public class UploadFileCommand: IRequest<FileResult>
{
   public FileData File { get; set; }
}