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
            // Click the anchor that wraps the Assets tile (reliable for the dashboard layout)
            await page.Locator("a:has(div:has-text(\"Assets\"))").ClickAsync();

        }
    }
}
