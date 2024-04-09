
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SohatNoteBook.Entities.DbSet;

namespace SohatNoteBook.DataService.Data
{
    public class AppDbContext : IdentityDbContext{
        public virtual DbSet<User> Users {get;set;}
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options){}
    }
}