using System;
using System.Drawing;
using System.IO;
using System.Net;

namespace Api.Service.Util
{
    public class ConvertUrlToBase64
    {
        public static string ConvertToBase64(string url)
        {
            byte[] _byte = GetImage(url);
            return ImageResize(Convert.ToBase64String(_byte, 0, _byte.Length));
        }

        public static string ImageResize(string base64)
        {
            byte[] data = Convert.FromBase64String(base64);

            using (var ms = new MemoryStream(data))
            {
                var image = Image.FromStream(ms);

                var ratioX = (double)150 / image.Width;
                var ratioY = (double)50 / image.Height;
                var ratio = Math.Min(ratioX, ratioY);

                var width = (int)(image.Width * ratio);
                var height = (int)(image.Height * ratio);

                var newImage = new Bitmap(width, height);
                Graphics.FromImage(newImage).DrawImage(image, 0, 0, width, height);
                Bitmap bmp = new(newImage);

                ImageConverter converter = new();

                data = (byte[])converter.ConvertTo(bmp, typeof(byte[]));

                return Convert.ToBase64String(data);
            }
        }

        private static byte[] GetImage(string url)
        {
            byte[] buf;
            try
            {
                WebProxy myProxy = new();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                Stream stream = response.GetResponseStream();

                using (BinaryReader br = new(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception)
            {
                buf = null;
            }

            return buf;
        }
    }
}
