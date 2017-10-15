using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Models
{
    public class FormModel
    {
        private readonly Lazy<List<FieldModel>> _fields;

        public FormModel()
        {
            _fields = new Lazy<List<FieldModel>>(() => GetFields().ToList());
        }

        public List<FieldModel> Fields => _fields.Value;

        private static IEnumerable<FieldModel> GetFields()
        {
            using (var db = new DataContext())
            {
                var picModel = new ImageModel();
                foreach (var p in db.Field.ToList().OrderBy(p => p.Id).Select(p => (FieldModel)p))
                {
                    p.PicUrl = picModel.Pictures.FirstOrDefault(im => im.Id == p.PictureId)?.Url;
                    picModel.GetImage(p);
                    yield return p;
                }
            }
        }
    }
}