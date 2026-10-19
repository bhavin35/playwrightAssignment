using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace assignment.pages
{
    public class LoginPage
    {
        private readonly IPage page;

        public LoginPage(IPage page)
        {
            this.page = page;

        }

        public async Task Login(string username, string password)
        {
            await page.GotoAsync("https://demo.snipeitapp.com/login");
            await page.Locator("#username").FillAsync("admin");
            await page.Locator("#password").FillAsync("password");
            await page.Locator("#submit").ClickAsync();

        }
    }
}
