using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } //string ve sınıflar referans tiplerdir. Bu yüzden tanımlanırken değer tipler gibi default değerlere sahip olmazlar. Her seferinde nullable olarak tanımlamaktansa bu uyarıyı susturdum.
        public int Stock { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; } // sınıfismi ve id yazınca ef core bunu otomatik algılıyor ve foreign key olarak atıyor.
        //Nav Propertyleri
        public Category Category { get; set; }

        public ProductFeature ProductFeature { get; set; }

    }
}
