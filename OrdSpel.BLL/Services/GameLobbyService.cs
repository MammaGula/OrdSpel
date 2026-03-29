using Microsoft.EntityFrameworkCore;
using OrdSpel.DAL.Data;
using OrdSpel.Shared;
using OrdSpel.Shared.DTOs;
using System.Threading.Tasks;

namespace OrdSpel.BLL.Services
{
    public class GameLobbyService : IGameLobbyService
    {
        private readonly AppDbContext _context;

        public GameLobbyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GameLobbyStatusDto?> GetLobbyStatusAsync(string gameCode)
        {
            if (string.IsNullOrWhiteSpace(gameCode))
            {
                return null;
            }

            var session = await _context.GameSessions
                .Include(s => s.Category)
                .Include(s => s.Players)
                .FirstOrDefaultAsync(s => s.GameCode == gameCode);

            if (session == null)
            {
                return null;
            }

            return new GameLobbyStatusDto
            {
                SessionId = session.Id,
                GameCode = session.GameCode,
                CategoryName = session.Category?.Name ?? string.Empty,
                StartWord = session.StartWord,
                Status = session.Status,
                PlayerCount = session.Players.Count,
                MaxPlayers = 2,
                IsReadyToStart = session.Players.Count >= 2 && session.Status == GameStatus.InProgress,
                CurrentTurnUserId = session.CurrentTurnUserId,
              
            };
        }
    }
}