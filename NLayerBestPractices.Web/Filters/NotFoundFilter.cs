using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerBestPractices.Web.ApiService;
using NLayerBestPractices.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractices.Web.Filters
{
    public class NotFoundFilter: ActionFilterAttribute
    {
        private readonly CategoryApiService _categoryApiService;

        public NotFoundFilter(CategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {  
            int id = (int)context.ActionArguments.Values.FirstOrDefault();  //--> VALUES: Action'lara istek yapıldığı zaman parametrelere gelen değerleri alıyor.

            var product = await _categoryApiService.GetByIdAsync(id);

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
                errorDto.Errors.Add($"id'si {id} olan kategori veritabanında bulunamadı.");


                // Bu noktada artık Web projesi olduğundan dolayı JSON bir nesne dönemiruz. ErrorView dönmemiz gerekli.
                context.Result = new RedirectToActionResult("Error", "Home", errorDto);
            }
        }
    }
}
