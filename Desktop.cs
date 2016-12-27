using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Script.Serialization;

namespace Emrys.Desktop
{
    /// <summary>
    /// 设置桌面背景
    /// </summary>
    public class Desktop
    {

        public static bool SetDesktop()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                //获取本地路径
                string rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                // 获取图片路径
                string jpgPath = Path.Combine(rootPath, DateTime.Now.ToString("yyyyMMdd") + ".jpg");

                // 如果存在直接返回
                if (File.Exists(jpgPath)) return true;


                WebClient web = new WebClient();
                web.Encoding = Encoding.UTF8;
                string json = web.DownloadString("http://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1");

                // 获取图片信息
                var imageModel = js.Deserialize<Rootobject>(json).Images[0];

                // 获取图片URL
                var imageUrl = "http://cn.bing.com/" + imageModel.Url;

                // 保存图片
                web.DownloadFile(imageUrl, jpgPath);

                // 添加说明文字
                System.Drawing.Image image = System.Drawing.Image.FromFile(jpgPath);
                Bitmap bitmap = new Bitmap(image, image.Width, image.Height);
                Graphics g = Graphics.FromImage(bitmap);

                g.DrawString(imageModel.Copyright, new Font("微软雅黑", 18), new SolidBrush(Color.White), 20, 1000);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                image.Dispose();

                // 保存图片
                bitmap.Save(jpgPath, ImageFormat.Jpeg);
                g.Dispose();
                bitmap.Dispose();

                // 设置桌面背景
                SystemParametersInfo(20, 0, jpgPath, 0x01 | 0x02);

                return true;
            }
            catch
            {
                return false;
            }
        }


        [DllImport("user32.dll")]
        private static extern bool SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    }
}
