using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioBack.Models.Tokens
{
    public class ApplicationSettingsModel
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
    }
}
