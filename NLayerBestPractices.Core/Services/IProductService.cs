using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NLayerBestPractices.Core.Models;

namespace NLayerBestPractices.Core.Services
{
    public interface IProductService: IService<Product>
    {
        //Veri Tabanı ile ilgili olmayan Product'a özgü metotlarımızı buraya yazabiliriz.
        // Örneğin ControlInnerBarcode diye bir metot yazmış olalım bu barkot bir API sayesinde kontrol ediliyor olsun, veritabanına gitmeye
        // gerek kalmadığı için service katmanında iş halledilmiş olucak.

        Task<Product> GetWithCategoryByIdAsync(int productId);

    }
}
