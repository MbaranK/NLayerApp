using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder) // category tablosu property ayarları yapıyoruz. Göstermek için yoksa isimlendirmelerden dolayı herhangi bir ayar yapmamıza gerek yok.
        {
            builder.HasKey(x => x.Id); //  Id column'un primary key olduğunu belirttik.
            builder.Property(x => x.Id).UseIdentityColumn(); // hiçbirşey belirtmezsek Id değeri birer birer artacak.
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.ToTable("Categories"); //database kaydedilecek isim. Dbset propertysindeki değeri girdik. Bunu yazmamış olsakda yine tablo ismi categories olacaktı.
        }
    }
}
