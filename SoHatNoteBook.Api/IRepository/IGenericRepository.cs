
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
namespace SohatNoteBook.DataService.IRepository{
    public interface IGenericRepository<T> where T : class {
        // Get all entities 
        Task<IEnumerable<T>> All();
        // Get specific entity based on Id
        Task<T> GetById(Guid id);
        Task<bool> Add (T entity);
        Task<bool> Delete (Guid userId);
        //Update entity or add if it does not exist
        Task<bool> Update (T entity);

    }
}