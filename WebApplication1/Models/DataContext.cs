using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WebApplication1.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Field> Field { get; set; }
        public DbSet<Picture> Picture { get; set; }

        public DataContext() : base("FieldsContext") { }
    }

    public abstract class FieldBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public string ValidationString { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int PictureId => 1;
    }

    [Table("xgb_softline.field")]
    public class Field : FieldBase
    { }

    public class FieldModel : FieldBase // can not inherit from Field class
    {
        public string PicUrl { get; set; }
        public byte[] PictureBytes { get; set; }

        public static explicit operator FieldModel(Field f)
        {
            return new FieldModel()
            {
                Id = f.Id,
                Name = f.Name,
                Data = f.Data,
                ValidationString = f.ValidationString,
                X = f.X,
                X1 = f.X1,
                X2 = f.X2,
                Y = f.Y,
                Y1 = f.Y1,
                Y2 = f.Y2
            };
        }
    }

    [Table("xgb_softline.picture")]
    public class Picture
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}