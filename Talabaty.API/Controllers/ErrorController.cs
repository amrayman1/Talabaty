using Microsoft.AspNetCore.Mvc;
using Talabaty.API.Errors;

namespace Talabaty.API.Controllers
{
    [Route("error/{code}")]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
