using NLayerBestPractices.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractices.Web.DTOs
{
    public class ProductWithCategoryDto:ProductDto
    {
        //Product nesnesindeki Category ismi ile, burada bulunan CategoryDto ismi aynı olmalıdır.
        public CategoryDto Category { get; set; }
    }
}
