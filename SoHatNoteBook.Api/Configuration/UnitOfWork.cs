using SohatNoteBook.DataService.Data;
using SohatNoteBook.DataService.IRepository;
using SohatNoteBook.DataService.Repository;
using SoHatNoteBook.Api.IConfiguration;

namespace SoHatNoteBook.Api.Configuration{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context ;
        private readonly ILogger _logger;
        public IUserRepository Users {get; private set;}


        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory){
            _context = context;
            _logger = loggerFactory.CreateLogger("db_logs");
            Users = new UserRepository(context, _logger);
        }
        public async Task CompleteAsync(){
            await _context.SaveChangesAsync();    
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}