using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Data.Responses;

namespace CVBuilder.Application.Data.Services;

public class ImageService
{
    private const int ImageMinimumBytes = 512;
    private readonly FileService _fileService;
    private readonly IMapper _mapper;

    public ImageService(FileService fileService, IMapper mapper)
    {
        _fileService = fileService;
        _mapper = mapper;
    }
    
    public async Task<FileResult> UploadImage(FileData image)
    {
        var file = await _fileService.CreateFileAsync(image);
        var result = _mapper.Map<FileResult>(file);
        return result;
    }
    
     public bool IsImage(FileData postedFile)
    {
        //-------------------------------------------
        //  Check the image mime types
        //-------------------------------------------
        if (postedFile.ContentType.ToLower() != "image/jpg" &&
                    postedFile.ContentType.ToLower() != "image/jpeg" &&
                    postedFile.ContentType.ToLower() != "image/pjpeg" &&
                    postedFile.ContentType.ToLower() != "image/gif" &&
                    postedFile.ContentType.ToLower() != "image/x-png" &&
                    postedFile.ContentType.ToLower() != "image/png")
        {
            return false;
        }

        //-------------------------------------------
        //  Check the image extension
        //-------------------------------------------
        if (Path.GetExtension(postedFile.FileName)?.ToLower() != ".jpg"
            && Path.GetExtension(postedFile.FileName)?.ToLower() != ".png"
            && Path.GetExtension(postedFile.FileName)?.ToLower() != ".gif"
            && Path.GetExtension(postedFile.FileName)?.ToLower() != ".jpeg")
        {
            return false;
        }


        return true;
    }
}
