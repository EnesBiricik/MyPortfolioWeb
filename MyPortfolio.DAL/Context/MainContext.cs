using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Configurations;
using MyPortfolio.Entities.Concrete;

namespace MyPortfolio.DAL.Context
{
    public class MainContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PageSettings> PageSettings { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Statistic> Statistics { get; set; }

        /*public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new PageSettingsConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new ReplyConfiguration());
            modelBuilder.ApplyConfiguration(new SocialMediaConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyPortfolio;" +
            //        "Trusted_Connection = True;" +
            //        "MultipleActiveResultSets = true;" +
            //        "TrustServerCertificate = True;",
            //        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));

            optionsBuilder.UseSqlServer("",
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
