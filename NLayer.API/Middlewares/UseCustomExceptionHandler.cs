using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    public static class UseCustomExceptionHandler //Extension methodlar da class static olacak. 
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    //Response Body'nin json formatta olduğunu belirttik.
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>(); // Bu Interface üzerinden uygulamada fırlatılan hatayı alıyoruz.


                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };

                    context.Response.StatusCode = statusCode;

                    var response = CustomResponseDTO<NoContentDTO>.Fail(statusCode, exceptionFeature.Error.Message);

                    // oluşturduğumuz response'ı json formatına dönüştürdük.
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                });


            });
        }
    }
}
