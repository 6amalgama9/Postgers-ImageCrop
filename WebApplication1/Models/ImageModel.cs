using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ImageModel
    {
        private const string ImagePath = "/Content/";
        private readonly Lazy<List<Picture>> _pictures;
        public List<Picture> Pictures => _pictures.Value;

        public ImageModel()
        {
            _pictures = new Lazy<List<Picture>>(GetPictures);
        }

        public void GetImage(FieldModel field)
        {
            var url = HttpContext.Current.Server.MapPath($"{ImagePath}{field.PicUrl}");
            using (var map = new Bitmap(url))
            {
                Rectangle sourceRectangle = new Rectangle(field.X1, field.Y1, field.X2 - field.X1, field.Y2 - field.Y1);
                var sec = map.Clone(sourceRectangle, PixelFormat.DontCare);

                using (MemoryStream stream = new MemoryStream())
                {
                    sec.Save(stream, ImageFormat.Jpeg);
                    field.PictureBytes = stream.ToArray();
                }
            }
        }

        private static List<Picture> GetPictures()
        {
            using (var db = new DataContext())
            {
                return db.Picture.OrderBy(p => p.Id).ToList();
            }
        }
    }
}