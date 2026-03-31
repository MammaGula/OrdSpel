using System;
using System.Collections.Generic;
using System.Text;

namespace OrdSpel.Shared.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<WordDto> Words { get; set; } = new();
    }
}
