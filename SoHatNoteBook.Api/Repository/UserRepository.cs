

using Microsoft.EntityFrameworkCore;
using SohatNoteBook.DataService.Data;
using SohatNoteBook.DataService.IRepository;
using SohatNoteBook.Entities.DbSet;

namespace SohatNoteBook.DataService.Repository{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
            
        }
        public override async Task<IEnumerable<User>> All(){
            try
            {
                return await dbSet.Where(x => x.Status == 1).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method has generated an error", typeof(UserRepository));
                return new List<User>();
            }
        }

        public Task<User> GetUserPerEmailAddress(string email)
        {
            throw new NotImplementedException();
        }
    }
}