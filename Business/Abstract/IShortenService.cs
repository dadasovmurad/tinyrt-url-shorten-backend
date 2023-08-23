using Core.Utilities.Results;
using Entitites.Concrete;
using Entitites.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IShortenService
    {
        IDataResult<ShortenUrlForResponseDto> Create(ShortenUrlForCreateDto shortenUrl);
        IResult Delete(Guid id);        
        IResult VerifyPassword(string shortUrl,string password);
        IDataResult<string> GetDestination(string shortUrl);
        IDataResult<bool> CheckProtected(string shortUrl);
    }
}