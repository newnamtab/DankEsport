using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EsportProject.Models.DBmodels
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options)
            : base(options)
        { }
        public DbSet<News> News { get; set; }
    }
    public class News
    {
        public int NewsID { get; set; }
        public string Title { get; set; }
        public string subtitle { get; set; }
        public string Content { get; set; }
        public string imgURL { get; set; }//Link til wwwroot folderen med img - se gerne https://www.mikesdotnetting.com/article/288/uploading-files-with-asp-net-core-1-0-mvc for mere info om hvordan vi laver dette
        public DateTime CreateDate { get; set; }
    }
}
