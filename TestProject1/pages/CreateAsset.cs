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
            await page.Locator(".select2 - search__field").FillAsync(companyName);
            await page.GetByRole(AriaRole.Option, new() { Name = companyName, Exact = false }).ClickAsync();
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
            await page.Locator(".select2 - search__field").FillAsync(modelName);
            await page.GetByRole(AriaRole.Option, new() { Name = modelName, Exact = false }).ClickAsync();
        }

        public async Task SaveAsset()
        {
            await page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
        }
    }
}
