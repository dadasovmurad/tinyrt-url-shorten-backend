﻿using Core.DataAccess;
using Entitites.Concrete;
using Entitites.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IShortenDal : IEntityRepository<ShortenUrl>
    {
        public IList<string> GetShortUrlList();
    }
}
