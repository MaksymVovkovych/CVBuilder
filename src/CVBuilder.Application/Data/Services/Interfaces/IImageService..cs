using System.Threading.Tasks;
using CVBuilder.Application.Data.Responses;

namespace CVBuilder.Application.Data.Services.Interfaces;

public interface IImageService
{
    Task<FileResult> UploadImage(string fileType, byte[] image);
}