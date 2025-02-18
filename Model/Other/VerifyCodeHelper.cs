using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dm.net.buffer.ByteArrayBuffer;

namespace Model.Other
{
    public class VerifyCodeHelper
    {

        public static string BuildCode(out string code)
        {
            code = string.Empty;
            Bitmap bitmap = new Bitmap(85, 30);//设置宽高
            Graphics graphics = Graphics.FromImage(bitmap);//生成画布
            graphics.Clear(Color.White);//清空画布

            //string letters = "023456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";//去除 1 I l 
            string letters = "0123456789";//去除 1 I l 
            Random random = new Random();
            //生成四位验证码
            for (int i = 0; i < 4; i++)
            {
                code += letters[random.Next(0, letters.Length)];
            }

            //画5条干扰线
            for (int i = 0; i < 5; i++)
            {
                int x1 = random.Next(bitmap.Width);
                int y1 = random.Next(bitmap.Height);
                int x2 = random.Next(bitmap.Width);
                int y2 = random.Next(bitmap.Height);
                graphics.DrawLine(new Pen(Color.Coral), x1, y1, x2, y2);
            }
            //画150个干扰点
            for (int i = 0; i < 150; i++)
            {
                int x1 = random.Next(bitmap.Width);
                int y1 = random.Next(bitmap.Height);
                bitmap.SetPixel(x1, y1, Color.FromArgb(random.Next()));//随机颜色填充
            }
            //画个边框
            graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, bitmap.Width - 1, bitmap.Height - 1));

            int randAngle = 45;//随机转动角度 //验证码旋转，防止机器识别
            char[] chars = code.ToCharArray();//拆散字符串成单字符数组
                                              //文字居中
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            //定义颜色
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //定义字体
            string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            for (int i = 0; i < chars.Length; i++)
            {
                int cindex = random.Next(7);
                int findex = random.Next(5);
                //字体样式(参数2为字体大小)
                Font f = new System.Drawing.Font(font[findex], 13, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                Point dot = new Point(16, 16);

                float angle = random.Next(-randAngle, randAngle);//转动的度数
                graphics.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
                graphics.RotateTransform(angle);
                graphics.DrawString(chars[i].ToString(), f, b, 1, 1, format);

                graphics.RotateTransform(-angle);//转回去
                graphics.TranslateTransform(2, -dot.Y);//移动光标到指定位置
            }
            string strbaser64 = string.Empty;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(arr, 0, (int)stream.Length);
                strbaser64 = Convert.ToBase64String(arr);
            }
            return strbaser64;
        }

    }
}
