using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction] //Endpoint olmadığını belirtmemiz lazım, yoksa hata alırız.
        public IActionResult CreateActionResult<T>(CustomResponseDTO<T> response)
        {
            if (response.StatusCode == 204) // No Content
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };

        
                
            


        }
    }
}
