using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitites.DTOs
{
    public class ProtectedUrlDto: IDto
    {
        public string ShortUrl { get; set; }
        public string Password { get; set; }
    }
}
