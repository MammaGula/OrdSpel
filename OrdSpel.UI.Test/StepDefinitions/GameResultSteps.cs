using Microsoft.Playwright;
using OrdSpel.PlaywrightTests.Helpers;
using Reqnroll;

namespace OrdSpel.PlaywrightTests.StepDefinitions;

[Binding]
public class GameResultSteps
{
    private readonly IPage _page;
    private readonly string _baseUrl;
    private readonly AuthHelper _authHelper;

    public GameResultSteps(Hooks.Hooks hooks)
    {
        _page = hooks.Page;
        _baseUrl = hooks.BaseUrl;
        _authHelper = new AuthHelper(_page, _baseUrl);
    }


    [Scope(Tag = "Result")]
    [Given(@"I am logged in as ""(.*)"" with password ""(.*)""")]
    public async Task GivenIAmLoggedInAsWithPassword(string username, string password)
    {
        await _authHelper.LoginAsync(username, password);
    }

    // Navigation step intentionally omitted here to avoid ambiguous step definitions.
    // Use the shared navigation step defined in other step definition classes.
    //Common steps(login, navigation) are defined in CreateJoinGameSteps and LobbySteps.
    //This class should only contain assertions specific to game result page.


    [Then("I should see the game result load error message")]
    public async Task ThenIShouldSeeTheGameResultLoadErrorMessage()
    {
        try
        {
            // GameResult component uses /game/{code} route and shows error when result cannot be loaded
            await _page.WaitForSelectorAsync("#gameResultError", new PageWaitForSelectorOptions { Timeout = 60000 });
            var text = await _page.TextContentAsync("#gameResultError");
            Assert.That(text, Does.Contain("Kunde inte ladda resultatet").Or.Contain("Kunde inte hämta resultat"));
        }
        catch (Exception)
        {
            var path = $"game_result_failure_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            try { await _page.ScreenshotAsync(new PageScreenshotOptions { Path = path, FullPage = true }); } catch { }
            throw;
        }
    }
}
