using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NLayerBestPractices.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractices.API.Filters
{
    public class ValidationFilter: ActionFilterAttribute
    {
        //Override'lar içerinde filterları ne zaman kullanmka istediğimize göre seçenekler bulunmaktadır. action çalışırken, action çalışması bitirdiği zaman vs...
        public override void OnActionExecuting(ActionExecutingContext context) //--> request ilgili nesneye geldiği zaman...
        {
            if(!context.ModelState.IsValid)
            {
                ErrorDto errorDto = new ErrorDto();

                errorDto.Status = 400;

                IEnumerable<ModelError> modelErrors = context.ModelState.Values.SelectMany(v => v.Errors);

                modelErrors.ToList().ForEach(
                    x =>
                    {
                        errorDto.Errors.Add(x.ErrorMessage);
                    }); 
                context.Result = new BadRequestObjectResult(errorDto); //--> BadRequestObjectResult ???????
            }
        }



    }
}
