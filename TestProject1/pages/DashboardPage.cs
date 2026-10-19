using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace assignment.pages
{
    public class DashboardPage
    {
        private readonly IPage page;

        public DashboardPage(IPage page)
        {
            this.page = page;

        }

        public async Task GoToAsset()
        {
          
            await page.Locator("a:has(div:has-text(\"Assets\"))").ClickAsync();

        }
    }
}
