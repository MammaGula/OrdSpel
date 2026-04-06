using Microsoft.Playwright;
using OrdSpel.PlaywrightTests.Hooks;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdSpel.PlaywrightTests.StepDefinitions
{
    [Binding]
    public class TurnSteps
    {
        private readonly IPage _page;
        private readonly string _baseUrl;

        public TurnSteps(Hooks.Hooks hooks)
        {
            _page = hooks.Page;
            _baseUrl = hooks.BaseUrl;
        }

        [Given("the start word is {string}")]
        public async Task GivenTheStartWordIsString()
        {
            
        }
    }
}

//INTE KLAR