using OrdSpel.Shared.DTOs;
using OrdSpel.Shared;
using System.Threading.Tasks;

namespace OrdSpel.BLL.Interfaces
{
    public interface IGameLobbyService
    {
        Task<ServiceResult<GameLobbyStatusDto>> GetLobbyStatusAsync(string gameCode);
    }
}
