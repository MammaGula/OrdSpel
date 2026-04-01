using OrdSpel.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdSpel.DAL.Repositories.Interfaces
{
    public interface IWordRepository
    {
        Task<List<Word>> GetAllAsync();
    }
}
