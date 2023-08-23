using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetResponse<T>(IDataResult<T> result)
        {
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetResponse(Core.Utilities.Results.IResult result)
        {
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
