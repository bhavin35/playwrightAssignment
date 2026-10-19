using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace assignment.pages
{
    public class CreateAsset
    {
        private readonly IPage page;

        public CreateAsset(IPage page)
        {
            this.page = page;

        }

        public async Task SelectCompany(String companyName)
        {
            await page.Locator("#select2-company_id_select-container").ClickAsync();
            await page.Locator(".select2-search__field").FillAsync(companyName);
            var optionSelector = $".select2-results__option:has-text(\"{companyName}\")";
            await page.WaitForSelectorAsync(optionSelector);
            await page.Locator(optionSelector).ClickAsync();
        }
        public async Task AddTag(String tag)
        {
            await page.Locator("#asset_tag").FillAsync(tag);
        }
        public async Task AddSerial(String serial)
        {
            await page.Locator("#serial_1").FillAsync(serial);
        }
        public async Task SelectModel(String modelName)
        {
            await page.Locator("#select2-model_select_id-container").ClickAsync();
            await page.Locator(".select2-search__field").FillAsync(modelName);
            var optionSelector = $".select2-results__option:has-text(\"{modelName}\")";
            await page.WaitForSelectorAsync(optionSelector);
            await page.Locator(optionSelector).ClickAsync();
        }

        public async Task SaveAsset()
        {
           
            await page.Locator("#submit_button").First.ClickAsync();
        }
    }
}
