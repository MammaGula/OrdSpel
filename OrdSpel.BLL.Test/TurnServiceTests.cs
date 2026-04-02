using Moq;
using OrdSpel.BLL.Services;
using OrdSpel.DAL.Models;
using OrdSpel.DAL.Repositories.Interfaces;
using OrdSpel.Shared.Constraints;
using OrdSpel.Shared.DTOs;
using OrdSpel.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OrdSpel.BLL.Test
{
    public class TurnServiceTests
    {
        //använd mockdata i tester
        //Skapa en mock av ITurnRepository för att testa utan riktig databas
        private readonly Mock<ITurnRepository> _repoMock;
        private readonly TurnService _turnService;

        public TurnServiceTests()
        {
            _repoMock = new Mock<ITurnRepository>();
            _turnService = new TurnService(_repoMock.Object);
        }

        [Fact]
        public async Task PlayTurnAsync_SessionSuccess()
        {
            //setup, skapa en ny session att använda i testet
            var session = new GameSession
            {
                Id = 1,
                GameCode = "ABC",
                Status = GameStatus.InProgress,
                CurrentTurnUserId = "user1",
                CurrentRound = 1,
                CategoryId = 1,
                StartWord = "katt",
                Players = new List<GamePlayer>
                {
                    new GamePlayer { UserId = "user1", TotalScore = 0 },
                    new GamePlayer { UserId = "user2", TotalScore = 0 }
                },
                Turns = new List<GameTurn>()
            };

            //returnerar gamesessionen ovan när GetSessionWithDetailsAsync anropas med gamecode ABC
            _repoMock.Setup(r => r.GetSessionWithDetailsAsync("ABC"))
                .ReturnsAsync(session);

            //simulerar att databasen innehåller ordet "tiger" i kategori 1 så att spelaren kna välja tiger som input
            _repoMock.Setup(r => r.GetWordAsync("tiger", 1))
                .ReturnsAsync(new Word
                {
                    Text = "tiger",
                    CategoryId = 1,
                    IsHard = false
                });

            //simulerar en spelares input  
            var dto = new TurnRequestDto
            {
                Word = "tiger",
                PassedTurn = false
            };

            //använder metoden PlayTurnAsync tillsammans med gamecode, användarens id och användarens ord för att få tillbaka response eller error
            var (response, error) = await _turnService.PlayTurnAsync("ABC", "user1", dto);

            // testen
            Assert.Null(error); //förväntar att error ska vara null (vi förväntar oss att detta testa ska vara succuessful)
            Assert.NotNull(response); //förväntar att response inte ska vara null
            Assert.Equal("user2", response.NextUserId); //förväntar att användare 2 ska vara samma som nästa användare eftersom det nyss har varit user1 som spelat
            Assert.Equal(2, response.CurrentRound); //spelet börjar på 1 (se start av test) och i metoden PlayTurnAsync ökar currentround med 1 efetr varje spelare har spelat/stått över sin tur och nu är det spelare2s tur så det är runda 2
        }
    }
}
