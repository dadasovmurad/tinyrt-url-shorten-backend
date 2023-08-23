using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Core.Utilities.Results;
using Entitites.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ShortenUrlValidator : AbstractValidator<ShortenUrlForCreateDto>
    {
        public ShortenUrlValidator()
        {
            RuleFor(x => x.Destination).NotEmpty().NotNull().Must(IsValidUrl).WithMessage(Messages.IncorrectUrl);
           
            RuleFor(x => x.ShortUrl).MaximumLength(20).Must(ShortUrlValidate);
        }

        private bool ShortUrlValidate(string shortUrl)
        {
            if (!string.IsNullOrWhiteSpace(shortUrl))
            {
                return shortUrl.Length >= 3;
            }
            return true;
        }
        private bool IsValidUrl(string url) => Uri.IsWellFormedUriString(url, UriKind.Absolute);
    }
}
