using Azure;
using Business.Abstract;
using Core.Utilities.Results;
using Entitites.Concrete;
using Entitites.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : BaseApiController
    {
        private readonly IShortenService _shortenService;

        public LinkController(IShortenService shortenService)
        {
            _shortenService = shortenService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ShortenUrlForCreateDto shortenUrl)
        {
            var response = _shortenService.Create(shortenUrl);
            
            return GetResponse(response);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Guid id)
        {
            var response = _shortenService.Delete(id);
            return GetResponse(response);
        }
        [HttpGet("check-protected")]
        public IActionResult CheckProtected(string shortUrl)
        {
            return GetResponse(_shortenService.CheckProtected(shortUrl));
        }
        [HttpGet("verify-password")]
        public IActionResult VerifyPassword(string shortUrl, string password)
        {
            var response = _shortenService.VerifyPassword(shortUrl,password);
            return GetResponse(response);
        }
        [HttpGet("destination")]
        public IActionResult GetDestination(string shortUrl)
        {
            var responseModel = _shortenService.GetDestination(shortUrl);
            return GetResponse(responseModel);
        }
    }
}