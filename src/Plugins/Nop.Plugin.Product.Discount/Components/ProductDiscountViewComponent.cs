using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Product.Discount.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Product.Discount.Components
{

    [ViewComponent(Name = "ProductDiscount")]
    public class ProductDiscountViewComponent : NopViewComponent
    {
        private readonly ProductDiscountSettings _productDiscountSettings;
        private readonly ILocalizationService _localizationService;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;

        public ProductDiscountViewComponent(ProductDiscountSettings productDiscountSettings,
            ILocalizationService localizationService,
            IStoreContext storeContext,
            IWorkContext workContext)
        {
            _productDiscountSettings = productDiscountSettings;
            _localizationService = localizationService;
            _storeContext = storeContext;
            _workContext = workContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new ConfigurationModel
            {
                MessageText = await _localizationService.GetLocalizedSettingAsync(_productDiscountSettings,
                              x => x.MessageText, (await _workContext.GetWorkingLanguageAsync()).Id, (await _storeContext.GetCurrentStoreAsync()).Id)
            };

            return View("~/Plugins/Product.Discount/Views/PublicInfo.cshtml", model);
        }
    }

}
