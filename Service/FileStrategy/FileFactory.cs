using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FileStrategy
{
    /// <summary>
    /// 工厂类，负责对象的实例化
    /// </summary>
    public class FileFactory
    {
        public static Strategy CreateStrategy(UploadMode mode)
        {
            switch (mode)
            {
                case UploadMode.Aws:
                    return new AwsStrategy();
                case UploadMode.Local:
                    return new LocalStrategy();
                default:
                    return new AwsStrategy();
            }
        }
    }
}
