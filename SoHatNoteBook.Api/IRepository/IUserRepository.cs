
using SohatNoteBook.Entities.DbSet;

namespace SohatNoteBook.DataService.IRepository{
    public interface IUserRepository:IGenericRepository<User>  {
        Task<User> GetUserPerEmailAddress(string email);
    }
}