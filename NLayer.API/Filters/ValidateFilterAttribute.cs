using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters
{
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)//Fluent validation da yazmış olduğumuz koşuller(Notnull, inclusiveBetween vb.), ModelState 'e yükleniyor. Yani koşullardan biri sağlanmazsa modelstate false'a düşüyor.
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(CustomResponseDTO<NoContentDTO>.Fail(400, errors)); //Response ' ın bodysinde hata mesajlarını göstermek istediğimiz için Object olanı seçtik

            } 

        }


    }
}
