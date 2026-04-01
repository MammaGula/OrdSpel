using OrdSpel.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdSpel.BLL.Services
{
    public interface IWordService
    {
        Task<List<WordDto>> GetAllAsync();
    }
}
