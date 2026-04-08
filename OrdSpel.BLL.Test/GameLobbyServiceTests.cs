using Microsoft.EntityFrameworkCore;
using OrdSpel.BLL.Services;
using OrdSpel.DAL.Data;
using OrdSpel.DAL.Models;
using OrdSpel.DAL.Repositories;
using OrdSpel.Shared.Enums;


namespace OrdSpel.BLL.Test
{
    public class GameLobbyServiceTests
    {
        // Use EF Core InMemory Database to simulate an in‑memory database.
        // Each test creates its own fresh database instance, keeping tests isolated so they don’t interfere with one another.
        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }



        // Test 1: returns null when the game code does not exist in the database.
        [Fact]
        public async Task GetLobbyStatusAsync_ReturnsFailResult_WhenGameCodeDoesNotExist()
        {
            // Arrange
            var context = CreateContext();
            var repo = new GameSessionRepository(context);
            var service = new GameLobbyService(repo);

            // Act
            var result = await service.GetLobbyStatusAsync("NOPE");

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }



        // Test 2: If the session and all required data are present, return the correct DTO.
        [Fact]
        public async Task GetLobbyStatusAsync_ReturnsLobbyStatus_WhenSessionExists()
        {
            // Arrange
            var context = CreateContext();

            // Create a category "Djur"
            var category = new Category
            {
                Name = "Djur"
            };

            context.Categories.Add(category);
            await context.SaveChangesAsync();


            // Create a game session associating with the category and a start word "hund"
            var session = new GameSession
            {
                GameCode = "ABC123",
                Status = GameStatus.WaitingForPlayers,
                CategoryId = category.Id,
                Category = category,
                StartWord = "hund",
                CurrentRound = 1,
                CreatedAt = DateTime.UtcNow
            };

            context.GameSessions.Add(session);
            await context.SaveChangesAsync();

            // Add one player to the session
            context.GamePlayers.Add(new GamePlayer
            {
                SessionId = session.Id,
                UserId = "user1",
                PlayerOrder = 1,
                TotalScore = 0
            });

            await context.SaveChangesAsync();

            // Call GetLobbyStatusAsyn and verify the returned DTO contains correct data based on the session and player we created.
            var repo = new GameSessionRepository(context);
            var service = new GameLobbyService(repo);



            // Act
            var result = await service.GetLobbyStatusAsync("ABC123");


            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("ABC123", result.Data!.GameCode);
            Assert.Equal("Djur", result.Data.CategoryName);
            Assert.Equal("hund", result.Data.StartWord);
            Assert.Equal(GameStatus.WaitingForPlayers, result.Data.Status);
            Assert.Equal(1, result.Data.PlayerCount);
            Assert.False(result.Data.IsReadyToStart);
        }




        // Test 3: 2 players + status is WaitingForPlayers => IsReadyToStart should be true
        [Fact]
        public async Task GetLobbyStatusAsync_IsReadyToStart_True_WhenTwoPlayersAndWaitingForPlayers()
        {
            // Arrange: Create a simulated database context 
            var context = CreateContext();

            // create a category (for GameSession can refer to CategoryId)
            var category = new Category
            {
                Name = "Djur"
            };

            context.Categories.Add(category);
            await context.SaveChangesAsync();

            // Create a game session (Status.WaitingForPlayers && startWord = "kat")
            var session = new GameSession
            {
                GameCode = "XYZ789",
                Status = GameStatus.WaitingForPlayers,
                CategoryId = category.Id,
                Category = category,
                StartWord = "katt",
                CurrentRound = 1,
                CurrentTurnUserId = "user1",
                CreatedAt = DateTime.UtcNow
            };

            context.GameSessions.Add(session);
            await context.SaveChangesAsync();

            // Add 2 players to the session
            context.GamePlayers.AddRange(
                new GamePlayer
                {
                    SessionId = session.Id,
                    UserId = "user1",
                    PlayerOrder = 1,
                    TotalScore = 0
                },
                new GamePlayer
                {
                    SessionId = session.Id,
                    UserId = "user2",
                    PlayerOrder = 2,
                    TotalScore = 0
                });

            await context.SaveChangesAsync();

            // Call Service to test
            var repo = new GameSessionRepository(context);
            var service = new GameLobbyService(repo);

            // Act: Call  to retrieve the lobby status for this game code
            var result = await service.GetLobbyStatusAsync("XYZ789");

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data!.PlayerCount);
            Assert.True(result.Data.IsReadyToStart);
            Assert.Equal(GameStatus.WaitingForPlayers, result.Data.Status);
            Assert.Equal("user1", result.Data.CurrentTurnUserId);
        }
    }
}