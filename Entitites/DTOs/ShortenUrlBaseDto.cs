using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitites.DTOs
{
    public class ShortenUrlBaseDto : IDto
    {
        public Guid Id { get; set; }
        public string Destination { get; set; }
        public string ShortUrl { get; set; }
    }
}
