using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entitites.Concrete;
using Entitites.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfShortenDal : EfEntityRepositoryBase<ShortenUrl, TinytrUrlShortenContext>, IShortenDal
    {
        public IList<string> GetShortUrlList()
        {
            using (TinytrUrlShortenContext context = new TinytrUrlShortenContext())
            {
                return context.ShortenUrls.Select(x => x.ShortUrl).ToList();
            }
        }
    }
}
