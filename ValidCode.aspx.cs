using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;

public partial class ValidCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string checkCode = CreateRandomCode(4);
        Session["CheckCode"] = checkCode;

        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ContentType = "image/Jpeg";
        HttpContext.Current.Response.BinaryWrite( CreateImage(checkCode).ToArray() );
    }

    private string CreateRandomCode(int codeCount)
    {
        const string RandomCodeChar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
        string[] allCharArray = RandomCodeChar.Split(',');
        int aryLen = (RandomCodeChar.Length - 1) / 2 + 1;
        string randomCode = string.Empty;
        int temp = -1;

        Random rand = new Random();
        for (int i = 0; i < codeCount; i++)
        {
            if (temp != -1)
            {
                rand = new Random(i * temp * ((int)DateTime.UtcNow.Ticks));
            }
            int t = rand.Next(aryLen);
            temp = t;
            randomCode += allCharArray[t];
        }
        return randomCode;
    }

    private MemoryStream CreateImage(string checkCode)
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
