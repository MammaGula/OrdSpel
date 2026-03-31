using Moq;
using OrdSpel.BLL.Services;
using OrdSpel.DAL.Models;
using OrdSpel.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdSpel.BLL.Test
{
    public class CategoryServiceTest
    {
        private readonly Mock<ICategoryRepository> _mockRepo;
        private readonly CategoryService _service;

        //sätter detta i konstruktorn så att det skapas på nytt inför varje test
        public CategoryServiceTest()
        {
            _mockRepo = new Mock<ICategoryRepository>(); //skapa en fejkversion av ICategoryRepository mha Moq (nuget paket)
            _service = new CategoryService(_mockRepo.Object); //.object är instansen av fejkrepository vi skickar till CategoryService
        }

        [Fact]
        public async Task GetAllCategoriesAsync()
        {
            // setup (fejkdata)
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Djur" },
                new Category { Id = 2, Name = "Länder" },
                new Category { Id = 3, Name = "Frukter och grönsaker" }
            };

            //anropar GetAllAsync från fejkrepositoriet
            _mockRepo.Setup(r => r.GetAllAsync())
                     .ReturnsAsync(categories);

            //anropar riktiga categoryservice som sen anropar fejrepositoryt (se konstruktor)
            var result = await _service.GetAllAsync();

            //förväntas vara 3 kategorier
            Assert.Equal(3, result.Count);
        }

        [Fact] //testar att det inte finns en kategori med id 4
        public async Task GetWordsByCategoryIdFail()
        {
            // setup (finns inte 4 kategorier)
            _mockRepo.Setup(r => r.GetByIdAsync(4))
                     .ReturnsAsync((Category?)null);

            //anropar riktiga categoryservice som sen anropar fejrepositoryt (se konstruktor)
            var result = await _service.GetWordsByCategoryIdAsync(4);

            // ska returnera null efetrsom kategori 4 inte finns
            Assert.Null(result);
        }

        [Fact]
        public async Task GetWordsByCategoryIdSuccess()
        {
            //setup - skapa fejkkategori och 2 fejkord som tillhör fejkkategorin
            var category = new Category { Id = 1, Name = "Djur" };
            var words = new List<Word>
            {
                new Word { Id = 1, Text = "lejon", CategoryId = 1 },
                new Word { Id = 2, Text = "tiger", CategoryId = 1 }
            };

            //returnera category och tillhörande words
            _mockRepo.Setup(r => r.GetByIdAsync(1))
                     .ReturnsAsync(category);
            _mockRepo.Setup(r => r.GetWordsByCategoryIdAsync(1))
                     .ReturnsAsync(words);

            var result = await _service.GetWordsByCategoryIdAsync(1);

            Assert.NotNull(result); //kontrollera att det inte är null
            Assert.Equal(2, result.Count); //kontrollera att det stämmer med 2 ord
        }
    }
}
