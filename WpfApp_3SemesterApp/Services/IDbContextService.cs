using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_3SemesterApp.Services
{
    public interface IDbContextService<T>
    {
        /// <summary>
        /// Gets all existing entities.
        /// </summary>
        /// <returns>List of entities.</returns>
        List<T> GetAll();

        /// <summary>
        /// Create new entity.
        /// </summary>
        /// <param name="entity">Entity to save.</param>
        /// <returns>Created entity.</returns>
        T Create(T entity);

        /// <summary>
        /// Gets entity by id.
        /// </summary>
        /// <param name="id">Id of entity to get.</param>
        /// <returns>Found entity.</returns>
        T Read(int id);

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>Updated entity.</returns>
        T Update(T entity);

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="id">Id of entity to delete.</param>
        /// <returns>Whether action was successful.</returns>
        bool Delete(int id);
    }
}
