using System;
using System.IO;
using System.Threading.Tasks;
using CVBuilder.Application.Core.Settings;
using CVBuilder.Application.Data.Responses;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeTypes;

namespace CVBuilder.Application.Data.Services;

using Models.Entities;

public class FileService
{
    private readonly string _path;
    private readonly IRepository<File, int> _fileRepository;

    public FileService(IRepository<File, int> fileRepository, IWebHostEnvironment environment,
        IOptions<AppSettings> options)
    {
        _fileRepository = fileRepository;
        _path = Path.Combine(environment.WebRootPath, options.Value.FolderNameForFiles);
        if (!Directory.Exists(_path))
            Directory.CreateDirectory(_path);
    }

    public async Task<File> CreateFileAsync(FileData file)
    {
        if (file.FileStream is null)
            throw new ForbiddenException("File is empty");

        var extension = MimeTypeMap.GetExtension(file.ContentType);
        extension ??= Path.GetExtension(file.FileName) ?? string.Empty;

        var pathFile = $"{Guid.NewGuid()}{extension}";
        var path = $"{_path}/{pathFile}";

        var fileEntity = new File
        {
            ContentType = file.ContentType,
            FileName = file.FileName,
            Path = pathFile,
        };

        if (file.FileStream != null)
        {
            await CreateFileAsync(path, file.FileStream);
        }

        fileEntity = await _fileRepository.CreateAsync(fileEntity);
        return fileEntity;
    }

    private static async Task CreateFileAsync(string path, Stream file)
    {
        await using var fs = new FileStream(path, FileMode.OpenOrCreate);
        await file.CopyToAsync(fs);
        fs.Flush();
    }
}