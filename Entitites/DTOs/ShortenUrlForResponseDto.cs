using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitites.DTOs
{
    public class ShortenUrlForResponseDto : ShortenUrlBaseDto
    {
       public bool PasswordProtected { get; set; }
    }
}
