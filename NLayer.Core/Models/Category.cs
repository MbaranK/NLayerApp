using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } // bir kategorinin birden fazla kategorisi olabileceği için bu şekilde yazdık. Bire çok ilişki
    }
}
