using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    //productın ekstra özelliklerini kapsayacak olan sınıf.Product ile arasında birebir bir ilişki olacak.
    public class ProductFeature 
    {
        public int Id { get; set; } //Base entityden miras almadığı için Id si olacak.
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

    }
}
