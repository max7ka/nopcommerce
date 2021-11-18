using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Product.Discount.Models;
using Nop.Services.Configuration;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Product.Discount.Controllers
{
    [AutoValidateAntiforgeryToken]
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class ProductDiscountController : BasePluginController
    {

        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly INotificationService _notificationService;

        public ProductDiscountController(ISettingService settingService,
            INotificationService notificationService,
            IStoreContext storeContext)
        {
            _settingService = settingService;
            _storeContext = storeContext;
            _notificationService = notificationService;
        }

        public async Task<IActionResult> Configure()
        {
            /*if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();*/

            //load settings for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var productDiscountSettings = await _settingService.LoadSettingAsync<ProductDiscountSettings>(storeScope);

            var model = new ConfigurationModel
            {
                MessageText = productDiscountSettings.MessageText
            };

            if (storeScope > 0)
            {
                // override settings here based on store scope
                model.MessageText_OverrideForStore = await _settingService.SettingExistsAsync(productDiscountSettings, x => x.MessageText, storeScope);
            }

            return View("~/Plugins/Product.Discount/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model) 
        {
            //load settings for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var productDiscountSettings = await _settingService.LoadSettingAsync<ProductDiscountSettings>(storeScope);

            //save settings
            productDiscountSettings.MessageText = model.MessageText;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            await _settingService.SaveSettingOverridablePerStoreAsync(productDiscountSettings, x => x.MessageText, model.MessageText_OverrideForStore, storeScope, false);

            //now clear settings cache
            await _settingService.ClearCacheAsync();

            _notificationService.SuccessNotification("Saved");

            //return await Configure();
            return Ok();
        }
    }
}
