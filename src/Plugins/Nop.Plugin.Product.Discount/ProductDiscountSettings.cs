using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Configuration;

namespace Nop.Plugin.Product.Discount
{
    public class ProductDiscountSettings : ISettings
    {
        public string MessageText { get; set; }
    }
}
