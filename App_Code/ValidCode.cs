using System;
using System.Web;
using System.Data;
using System.Drawing;
using System.IO;

namespace IMSite.Data
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class ValidCodeBase
    {
        ValidCodeBase() { }
        /// <summary>
        /// 使用方法:
        /// HttpContext.Current.Response.ClearContent();
        /// HttpContext.Current.Response.ContentType = "image/Jpeg";
        /// HttpContext.Current.Response.BinaryWrite(
        ///     IMSite.Data.ValidCodeBase.CreateImage(checkCode).ToArray()
        ///     );
        /// </summary>
        /// <param name="checkCode"></param>
        /// <returns></returns>
        static public MemoryStream CreateImage1(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 12);
            Bitmap image = new Bitmap(iwidth, 20);
            Graphics g = Graphics.FromImage(image);
            Font f = new Font("Arial", 10, FontStyle.Bold);
            Brush b = new SolidBrush(Color.White);
            //g.FillRectangle(new System.Drawing.SolidBrush(Color.Blue),0,0,image.Width, image.Height);
            g.Clear(Color.Black);
            g.DrawString(checkCode, f, b, 3, 3);

            Pen blackPen = new Pen(Color.Bisque, 0);
            Random rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                int y = rand.Next(image.Height);
                g.DrawLine(blackPen, 0, y, image.Width, y);
            }

            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            g.Dispose();
            image.Dispose();
            return ms;
        }

        static public MemoryStream CreateImage(string checkCode)
        {
            if (string.IsNullOrEmpty(checkCode))
                return null;

            Bitmap image = new Bitmap((int)Math.Ceiling((checkCode.Length * 14.0)), 24);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                g.Clear(Color.White);
                Pen pen;
                //画图片的背景噪音线
                for (int i = 0; i < 0; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    pen = new Pen(Color.Silver);
                    g.DrawLine(pen, x1, y1, x2, y2);
                }

                Font font = new Font("Tahoma", 12, (FontStyle.Bold));
                //Font font = new Font("Arial Narrow", 14, (FontStyle.Bold));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Black, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                pen = new Pen(Color.Silver);
                //画图片的边框线
                //g.DrawRectangle(pen, 0, 0, image.Width - 1, image.Height - 1);

                MemoryStream ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                font.Dispose();
                pen.Dispose();
                return ms;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

    }
}