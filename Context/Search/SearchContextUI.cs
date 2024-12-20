﻿using Microsoft.Playwright;
using ReqnrollPlaywrightRestSharpDemo.UI;
using ReqnrollPlaywrightRestSharpDemo.UI.Pages;
using System.Diagnostics.CodeAnalysis;

namespace ReqnrollPlaywrightRestSharpDemo.Context.Search
{
    [method: SetsRequiredMembers]
    public class SearchContextUI(UIDriver uiDriver) : ISearchContext
    {
        private readonly ProductsPage _productsPage = new(uiDriver);

        public async Task AuthenticateUser()
        {
            //No authentication and authorisation is applicable in this case, so only navigating to page
            var response = await _productsPage.Navigate();

            response.Should().NotBeNull();
            response!.Status.Should().BeInRange(200, 299);

            await _productsPage.AcceptConsentIfVisible();
        }

        public async Task SearchForItem(string searchTerm)
        {
            await _productsPage.SearchForItem(searchTerm);
        }

        public async Task ValidateResult(string expectedDescription, string expectedPrice)
        {
            var productInfoItems = _productsPage.ProductInfoItemsFiltered([expectedDescription, expectedPrice]);

            await Assertions.Expect(productInfoItems).ToBeVisibleAsync();
        }

        public async Task ValidateResultExpected(int expectedItems)
        {
            var productInfoList = _productsPage.ProductInfoList();

            await Assertions.Expect(productInfoList).ToHaveCountAsync(expectedItems);
        }

        public async Task ValidateResultNotExpected(int notExpectedItems)
        {
            var productInfoList = _productsPage.ProductInfoList();

            await Assertions.Expect(productInfoList).Not.ToHaveCountAsync(notExpectedItems);
        }
    }
}
