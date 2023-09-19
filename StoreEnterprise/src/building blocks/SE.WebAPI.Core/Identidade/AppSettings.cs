using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SE.WebAPI.Core.Identidade
{
    public class AppSettings
    {
        public string Secret { get; set; } = null!;
        public int ExpiracaoHoras { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
    }
}