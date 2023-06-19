using Microsoft.AspNetCore.Mvc;
using Talabaty.API.Errors;
using Talabaty.DAL.Data;

namespace Talabaty.API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context) 
        { 
            _context = context;
        }

        [HttpGet("notFound")]
        public IActionResult GetNotFoundRequest()
        {
            var product = _context.Products.Find(50);

            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("serverError")]
        public IActionResult GetServerError()
        {
            var product = _context.Products.Find(50);

            var returnNull = product.ToString();

            return Ok();
        }
        
        [HttpGet("badRequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badRequest/{id}")]
        public IActionResult GetBadRequestById(int id)
        {
            return Ok();
        }


    }
}
