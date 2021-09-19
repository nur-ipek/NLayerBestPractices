using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractices.API.DTOs
{
    public class CategoryWithProductDto: CategoryDto
    {
        //CategoryDto sınıfındaki property isimleri Category sınıfındakiler ile aynı olmalıdır.
        public IEnumerable<ProductDto> Products { get; set; } 
    }
}

