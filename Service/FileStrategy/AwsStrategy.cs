using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FileStrategy
{
    public class AwsStrategy : Strategy
    {
        public override async Task<string> Upload(List<IFormFile> formFiles)
        {
            var res = await Task.Run(() => {
                //密钥
                List<string> result = new List<string>();
                foreach (var file in formFiles) { 
                if (file.Length > 0)
                    {
                        var filePath_temp = $"{AppContext.BaseDirectory}/Images_temp";
                        var fileName = $"/{DateTime.Now:yyyyMMddhhmmssfff}{file.FileName}";
                        if (!Directory.Exists(filePath_temp))
                        {
                            Directory.CreateDirectory(filePath_temp);
                        }
                        using (var stream = System.IO.File.Create(filePath_temp + fileName))
                        {
                            file.CopyTo(stream);
                        }
                        //上传的文件名
                        string key = fileName;
                        //本地文件路径
                        string filePath=$"{filePath_temp}/{fileName}";

                        // S3へ保存
                        var client = new AmazonS3Client();
                        var response = client.PutObjectAsync(new PutObjectRequest
                        {
                            BucketName = "mybucket",
                            Key = fileName,
                            FilePath = filePath,
                        });

                        //// S3に保存成功
                        //if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        //{
                        //    // オブジェクトのキーを返却
                        //    return new UploadOutput()
                        //    {
                        //        Key = fileName,
                        //    };
                        //    Directory.Delete(filePath_temp, true);
                        //}

                        //// S3に保存失敗
                        //// TODO: エラー原因をログに出したり、フロント側にエラーを伝えること
                        //return new UploadOutput();

                    }
                }

                return string.Join(",", result);
            });
            return res;
        }
    }
}
