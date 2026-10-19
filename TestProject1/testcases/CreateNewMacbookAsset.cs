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
        // Shared values between tests when running in sequence
        private static string createdSerial;
        private static string createdTag;
        private static string createdModel;
        private static string createdUser;

        [Test]
        [Order(1)]
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

            var tag = $"SNIPE-{DateTime.UtcNow:yyyyMMddHHmmss}";
            var serial = $"BHAVIN-SN-{DateTime.UtcNow:fff}";
            await createAsset.SelectCompany("Auer PLC");
            await createAsset.AddTag(tag);
            await createAsset.AddSerial(serial);
            await createAsset.SelectModel("Macbook Pro 13");
            await createAsset.SelectStatus("Ready to Deploy");
            await createAsset.SelectUser("Layla");
            await createAsset.SaveAsset();

            // store for subsequent test
            createdSerial = serial;
            createdTag = tag;
            createdModel = "Macbook Pro 13";
            createdUser = "Mikel";


        }

        [Test]
        [Order(2)]
        public async Task TC002()
        {
            // Search for the serial used in TC001 and verify it appears in the results table
            var login = new LoginPage(Page);
            var dashboard = new DashboardPage(Page);

            await login.Login("admin", "password");
            await Expect(Page).ToHaveTitleAsync("Dashboard :: Snipe-IT Asset Management Demo");

            await dashboard.GoToAsset();

            // Use the top lookup/search box to find the asset by serial (created in TC001)
            if (string.IsNullOrEmpty(createdSerial))
            {
                throw new InvalidOperationException("TC001 should run before TC002");
            }
            var serial = createdSerial;
            await Page.FillAsync("input[placeholder='Lookup by Asset Tag']", serial);
            await Page.Keyboard.PressAsync("Enter");

            // Wait for the assets table to show a row containing the serial
            await Page.WaitForSelectorAsync($"table tbody tr:has-text(\"{serial}\")");
            var row = Page.Locator($"table tbody tr:has-text(\"{serial}\")").First;
            await Expect(row).ToBeVisibleAsync();
            await Page.WaitForTimeoutAsync(3000);

        }

        [Test]
        [Order(3)]
        public async Task TC003()
        {
            // Click the serial from the search results and verify details on the asset detail page
            var login = new LoginPage(Page);
            var dashboard = new DashboardPage(Page);

            await login.Login("admin", "password");
            await Expect(Page).ToHaveTitleAsync("Dashboard :: Snipe-IT Asset Management Demo");

            await dashboard.GoToAsset();

            if (string.IsNullOrEmpty(createdSerial) || string.IsNullOrEmpty(createdTag))
            {
                throw new InvalidOperationException("TC001 should run before TC003");
            }

            var serial = createdSerial;
            var tag = createdTag;

            // Search for the asset
            await Page.FillAsync("input[placeholder='Lookup by Asset Tag']", serial);
            await Page.Keyboard.PressAsync("Enter");

            // Wait for the row and click the serial link to navigate to detail page
            var row = Page.Locator($"table tbody tr:has-text(\"{serial}\")").First;
            await row.WaitForAsync();

            // Click and wait for load state instead of deprecated WaitForNavigationAsync
            await row.Locator($"a:has-text(\"{serial}\")").ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            
            var tagText = (await Page.Locator("dl.table-display .js-copy-asset_tag").InnerTextAsync())?.Trim();
            var serialText = (await Page.Locator("li#serial-number .js-copy-license_key code").InnerTextAsync())?.Trim();
            var modelText = (await Page.Locator("li#model .js-copy-asset_model").InnerTextAsync())?.Trim();
            var userText = (await Page.Locator(".col-md-4 .well a[href*='/users/']").InnerTextAsync())?.Trim();

            NUnit.Framework.Assert.That(tagText, NUnit.Framework.Is.EqualTo(tag), "Asset tag mismatch on detail page");
            NUnit.Framework.Assert.That(serialText, NUnit.Framework.Is.EqualTo(serial), "Serial mismatch on detail page");
            NUnit.Framework.Assert.That(modelText.Contains(createdModel), NUnit.Framework.Is.True, $"Model not found. Actual: '{modelText}'");
            NUnit.Framework.Assert.That(userText, NUnit.Framework.Is.Not.Null.And.Not.Empty, $"User not present on detail page. Actual: '{userText}'");
        }
    }
}

