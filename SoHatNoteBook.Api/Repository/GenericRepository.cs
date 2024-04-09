

using Microsoft.EntityFrameworkCore;
using SohatNoteBook.DataService.Data;
using SohatNoteBook.DataService.IRepository;

namespace SohatNoteBook.DataService.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext _contenxt;
        protected readonly ILogger _logger;
        internal DbSet<T> dbSet;
        private AppDbContext context;

        public GenericRepository(AppDbContext context, ILogger logger)
        {
            _contenxt = context;
            dbSet = context.Set<T>();
            _logger = logger;
        }

        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await dbSet.FindAsync(id);
   
            if (entity == null)
            {
                return false;
            }
            try
            {
                // Xóa đối tượng khỏi cơ sở dữ liệu
                dbSet.Remove(entity);
                await _contenxt.SaveChangesAsync();

                // Trả về true khi xóa thành công
                return true;
            }
            catch (Exception)
            {
                // Xử lý các ngoại lệ nếu có
                throw; // Bạn có thể xử lý ngoại lệ tại đây nếu cần thiết
            }
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}