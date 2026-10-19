using assignment.pages;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace assignment.testcases
{
    public class CreateNewMacbookAsset:PageTest
    {
        [Test]
        public async Task TC001()
        {
            var login = new LoginPage(Page);
            var dashboard = new DashboardPage(Page);
            var asset = new AssetPage(Page);
            var createAsset = new CreateAsset(Page);

            await login.Login("admin", "password");
            await Expect(Page).ToHaveTitleAsync("Dashboard :: Snipe-IT Asset Management Demo");

            await dashboard.GoToAsset();

            await asset.CreateAsset();

            await createAsset.SelectCompany("Adams Ltd");
            await createAsset.AddTag("SNIPE-1760431");
            await createAsset.AddSerial("test123");
            await createAsset.SelectModel("Macbook Pro 13");
            await createAsset.SaveAsset();


        }
    }
}

