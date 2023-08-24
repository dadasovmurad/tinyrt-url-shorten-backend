using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Exceptions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entitites.Concrete;
using Entitites.DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ShortenManager : IShortenService
    {
        private readonly IShortenDal _shortenDal;

        public ShortenManager(IShortenDal shortenDal)
        {
            _shortenDal = shortenDal;
        }
        [ValidationAspect(typeof(ShortenUrlValidator))]
        public IDataResult<ShortenUrlForResponseDto> Create(ShortenUrlForCreateDto urlForCreate)
        {
            var businessRules = BusinessRules.Run(CheckShortUrlExists(urlForCreate.ShortUrl));
            if (businessRules is null)
            {
                byte[] passwordHash = null, passwordSalt = null;

                if (!string.IsNullOrWhiteSpace(urlForCreate.Password))
                    HashingHelper.CreatePasswordHash(urlForCreate.Password, out passwordHash, out passwordSalt);

                urlForCreate.ShortUrl = string.IsNullOrWhiteSpace(urlForCreate.ShortUrl) ? GenerateUniqueUrl(5) : urlForCreate.ShortUrl;

                var generationUrl = new ShortenUrl
                {
                    Destination = urlForCreate.Destination,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    ShortUrl = urlForCreate.ShortUrl,
                };

                _shortenDal.Add(generationUrl);

                var responseModel = new ShortenUrlForResponseDto
                {
                    Id = generationUrl.Id,
                    Destination = generationUrl.Destination,
                    ShortUrl = generationUrl.ShortUrl,
                    PasswordProtected = !string.IsNullOrWhiteSpace(urlForCreate.Password)
                };
                return new SuccessDataResult<ShortenUrlForResponseDto>(responseModel, Messages.UrlCreated);
            }
            return new ErrorDataResult<ShortenUrlForResponseDto>(businessRules.Message);
        }

        private IResult CheckShortUrlExists(string shortUrl)
        {
            var checkToShortUrl = _shortenDal.GetList(x=>x.ShortUrl == shortUrl).Any();
            return checkToShortUrl ? new ErrorResult(Messages.ExistsShortUrl) : new SuccessResult();
        }

        public IResult Delete(Guid id)
        {
            if (HasUrl(id))
            {
                var deletedEntity = _shortenDal.Get(x => x.Id == id);
                _shortenDal.Delete(deletedEntity);
                return new SuccessResult(Messages.UrlDeleted);
            }
            throw new NotFoundException(Messages.UrlNotFound);
        }

        private string GenerateUniqueUrl(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string uniqueUrl = string.Empty;
            var shortUrlList = _shortenDal.GetList().Select(x=>x.ShortUrl).ToList();
            do
            {
                uniqueUrl = new string(Enumerable.Repeat(chars, length)
                                                 .Select(s => s[Random.Shared.Next(s.Length)])
                                                 .ToArray());
            } while (shortUrlList.Contains(uniqueUrl));

            return uniqueUrl;
        }
        public IResult VerifyPassword(string shortUrl, string password)
        {
            var checkToProtected = CheckProtected(shortUrl);
            if (checkToProtected.Data)
            {
                var urlToCheck = _shortenDal.Get(x => x.ShortUrl == shortUrl);
                if (!HashingHelper.VerifyPasswordHash(password, urlToCheck.PasswordHash, urlToCheck.PasswordSalt))
                    return new ErrorResult(Messages.IncorrectPassword);
            }
            else
                throw new NotFoundException(Messages.UrlNotFound);

            return new SuccessResult(Messages.CorrectPassword);
        }
        private bool HasUrl(Guid id)
        {
            return _shortenDal.GetList(x => x.Id == id).Any();
        }

        public IDataResult<bool> CheckProtected(string shortUrl)
        {
            var passToCheck = _shortenDal.Get(x => x.ShortUrl == shortUrl && x.PasswordHash != null);

            return new SuccessDataResult<bool>(passToCheck is not null);
        }

        public IDataResult<string> GetDestination(string shortUrl)
        {
            string? destination = _shortenDal.Get(x => x.ShortUrl == shortUrl)?.Destination;
            if (destination is not null)
                return new SuccessDataResult<string>(data: destination);

            throw new NotFoundException(Messages.UrlNotFound);
        }
    }
}