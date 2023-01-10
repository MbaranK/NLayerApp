using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity //Id ye erişmek için kullandık.
    {
        private readonly IService<T> _service;
                                                    //Eğer bir filter ctor'unda herhangi bir classı Dependency Injection kullanarak implement ediyorsa bunu program.cs tarafına eklememiz lazım. Aynı zamanda sadece ServiceFilter attribute'u üzerinden kullanabiliyoruz.       
        public NotFoundFilter(IService<T> service)
        {
           _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();

            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;

            var anyEntity = await _service.AnyAsync(x=>x.Id == id); // Id var mı yok mu kontrol ediyoruz.

            if(anyEntity)
            {
                await next.Invoke();
            }
            context.Result = new NotFoundObjectResult(CustomResponseDTO<NoContentDTO>.Fail(404, $"{typeof(T).Name}({id}) not found"));

        }
    }
}
