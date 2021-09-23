using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerBestPractices.API.DTOs;
using NLayerBestPractices.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractices.API.Filters
{
    public class NotFoundFilter: ActionFilterAttribute
    {
        private readonly IProductService _productService;

        public NotFoundFilter(IProductService productService)
        {
            _productService = productService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {  
            int id = (int)context.ActionArguments.Values.FirstOrDefault();  //--> VALUES: Action'lara istek yapıldığı zaman parametrelere gelen değerleri alıyor.

            var product = await _productService.GetByIdAsync(id);

            if(product!= null)
            {
                await next(); //istediği devam et !! Hangi action'ı tanımlarsam o metotun içine gir !!
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();

                //Client'ın yapmış olduğu bir hata oldu için status = 404
                errorDto.Status = 404;

                //Parantez içerisinde değişken kullanıceksak $ işareti koyuyoruz...
                errorDto.Errors.Add($"id'si {id} olan ürün veritabanında bulunamadı.");

                context.Result = new NotFoundObjectResult(errorDto);
            }
        }
    }
}
