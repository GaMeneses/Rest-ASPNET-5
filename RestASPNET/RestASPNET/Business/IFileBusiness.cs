using Microsoft.AspNetCore.Http;
using RestASPNET.Data.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestASPNET.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailVO> SaveFileDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
