using SohatNoteBook.DataService.IRepository;

namespace SoHatNoteBook.Api.IConfiguration{
    public interface IUnitOfWork{
        IUserRepository Users {get;}
        Task CompleteAsync ();
    }
 }