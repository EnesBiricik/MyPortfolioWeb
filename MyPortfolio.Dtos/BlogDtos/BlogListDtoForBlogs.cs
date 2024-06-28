using MyPortfolio.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Dtos
{
    public class BlogListDtoForBlogs : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string? CoverPhoto { get; set; }
        public DateTime Date { get; set; }
        public ushort ReadingTime { get; set; }
        public bool IsFeatured { get; set; }
    }
}
