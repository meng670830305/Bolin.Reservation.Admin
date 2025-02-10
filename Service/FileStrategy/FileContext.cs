using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FileStrategy
{
    /// <summary>
    /// 上下文，通过这个来调用具体策略
    /// </summary>
    public class FileContext
    {
        private Strategy _stragety;
        private List<IFormFile> _formFiles;
        public FileContext(Strategy stragety, List<IFormFile> formFiles)
        {
            _stragety = stragety;
            _formFiles = formFiles;
        }
    public async Task<string> ContextInterFace()
        {
            return await _stragety.Upload(_formFiles);
        }
    
    }
}
