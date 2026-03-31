using OrdSpel.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdSpel.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        //async metoder (Task) som måste användas i CategoryRepository 
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<List<Word>> GetWordsByCategoryIdAsync(int id);
    }
}
