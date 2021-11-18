using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Product.Discount.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Product.Discount.Components
{

    [ViewComponent(Name = "ProductDiscountAdmin")]
    public class ProductDiscountAdmin : NopViewComponent
    {
        private readonly ProductDiscountSettings _productDiscountSettings;

        public ProductDiscountAdmin(ProductDiscountSettings productDiscountSettings)
        {
            _productDiscountSettings = productDiscountSettings;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new ConfigurationModel
            {
                MessageText = _productDiscountSettings.MessageText
            };

            return View("~/Plugins/Product.Discount/Views/Configure.cshtml", model);
        }
    }

}
