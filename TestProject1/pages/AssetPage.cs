using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace assignment.pages
{
    public class AssetPage
    {
        private readonly IPage page;

        public AssetPage(IPage page)
        {
            this.page = page;

        }

        public async Task CreateAsset()
        {
            await page.Locator("button[name='btnAdd']").ClickAsync();

        }
    }
}
