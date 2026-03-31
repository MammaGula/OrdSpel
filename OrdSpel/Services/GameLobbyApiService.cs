using System.Net;
using OrdSpel.Shared.DTOs;

namespace OrdSpel.UI.Services
{
    public class GameLobbyApiService
    {
        private readonly HttpClient _httpClient;

        public GameLobbyApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GameLobbyStatusDto?> GetLobbyStatusAsync(string gameCode, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(gameCode))
            {
                return null;
            }

            // Call the API to get the lobby status for the specified game code
            // Uri.EscapeDataString(gameCode) for encoding the game code in case it contains special characters
            using var response = await _httpClient.GetAsync($"api/games/{Uri.EscapeDataString(gameCode)}/lobby", ct);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GameLobbyStatusDto>(ct);
        }
    }
}
