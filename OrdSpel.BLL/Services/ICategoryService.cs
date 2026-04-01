using OrdSpel.DAL.Models;
using OrdSpel.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdSpel.BLL.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<List<WordDto>?> GetWordsByCategoryIdAsync(int id);
    }
}
