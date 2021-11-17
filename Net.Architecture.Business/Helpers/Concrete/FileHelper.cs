using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Net.Architecture.Business.Abstract;
using Net.Architecture.Business.Helpers.Abstract;
using Net.Architecture.Core.Constants;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.DataAccess.UnitOfWork;
using File = Net.Architecture.Entities.Concrete.Common.File;

namespace Net.Architecture.Business.Helpers.Concrete
{
    public class FileHelper : BaseBusiness, IFileHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileHelper(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResult<File>> SaveFileToRoot(FileOperationsDto fileOperationsDto)
        {
            var fileType = fileOperationsDto.File.FormFile.FileName.Split(".").Last();
            var file = new File()
            {
                Status = true,
                Source = $"{Guid.NewGuid()}.{fileType}",
                FileType = fileType,
                Name = fileOperationsDto.File.FormFile.FileName.Split(".").First(),
                IsItPrivate = false
            };

            var controlFile = ControlFile(fileOperationsDto.File.FormFile, file);
            if (!controlFile.Result)
                return new ServiceResult<File>(controlFile.Message);

            await UploadFile(fileOperationsDto.File.FormFile, file.Source, $"{Constants.WWWRoot}/{Constants.Files}");

            await _unitOfWork.Repository<FileDal>().AddAsync(file);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceResult<File>(file);
        }

        private ServiceResult ControlFile(IFormFile formFile, File file)
        {
            if (!Constants.AcceptedFileExtensions.Contains(file.FileType.ToLower()))
                return new ServiceResult(Messages.FileExtensionError);

            if (formFile.Length > Constants.MaxFileSize)
                return new ServiceResult(Messages.FileSizeError);

            if (file.Name.Length > Constants.MaxFileNameSize)
                return new ServiceResult(Messages.FileNameSizeError);

            return new ServiceResult();
        }
        private async Task UploadFile(IFormFile file, string source, string filePath)
        {
            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), filePath, source);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public string GetRootUrlWithSource(string source)
        {
            var host = _httpContextAccessor.HttpContext.Request.Host;
            var schema = _httpContextAccessor.HttpContext.Request.Scheme;
            return $"{schema}://{host}//{Constants.Files}/{source}";
        }

        public async Task<ServiceResult<long>> GetFileIdByRootİmageUrl(string imageUrl)
        {
            var source = imageUrl.Split("/").Last();
            var file = await _unitOfWork.Repository<FileDal>().GetAsync(x => x.Status && x.Source == source);
            if (file is null)
                return new ServiceResult<long>(Messages.FileNotFound);

            return new ServiceResult<long>(file.Id);
        }
    }
}
