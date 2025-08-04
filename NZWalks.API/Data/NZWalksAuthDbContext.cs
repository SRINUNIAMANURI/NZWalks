using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> dbContext) : base(dbContext)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerId = ("5993E051-F7F1-4B89-93EA-E7F395461F9D");
            var writerId = ("EC11B02C-A2C6-4753-BC2F-DD32C4A95EF5");

            var values = new List<IdentityRole>
            { 
                new IdentityRole
                {
                    Id = readerId,
                    Name = "Reader",
                ConcurrencyStamp = readerId,
                NormalizedName = "Reader".ToUpper()
                },
                 new IdentityRole
                {
                    Id = writerId,
                    Name = "Writer",
                ConcurrencyStamp = writerId,
                NormalizedName = "Writer".ToUpper()
                },
            };
            builder.Entity<IdentityRole>().HasData(values);
        }
    }
}
