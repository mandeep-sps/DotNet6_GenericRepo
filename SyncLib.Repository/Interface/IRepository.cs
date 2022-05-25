using SyncLib.Repository.Database;
using System.Linq.Expressions;

namespace SyncLib.Repository.Interface
{
    /// <summary>
    /// Repository Interface
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Save all the changes to database ***Mandatory to call this function after every transaction.
        /// </summary>
        /// <returns></returns>
        public Task<bool> SaveChangesAsync();

        #region Get Methods

        /// <summary>
        ///  Get all data list of entity.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetAllAsync<T>() where T : class, IAudit;

        /// <summary>
        /// Get all data list of entity based on given condition.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> filter) where T : class, IAudit;

        /// <summary>
        /// Get all data list of entity based on given condition including join properties.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties) where T : class, IAudit;


        /// <summary>
        /// Get Entity based on given condition
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<T> GetAsync<T>(Expression<Func<T, bool>> filter) where T : class, IAudit;

        /// <summary>
        /// Get Entity based on given condition including joint properties.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public Task<T> GetAsync<T>(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties) where T : class, IAudit;
        #endregion


        #region Add Methods

        /// <summary>
        /// Add new entry
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createdby"></param>
        /// <returns></returns>
        public Task AddAsync<T>(T entity, int createdBy) where T : class, IAudit;

        /// <summary>
        /// Add new batch entry
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public Task AddRangeAsync<T>(IEnumerable<T> entities, int createdBy) where T : class, IAudit;
        #endregion


        #region Update Methods

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updatedBy"></param>
        public Task Update<T>(T entity, int updatedBy) where T : class, IAudit;

        /// <summary>
        /// Update batch at once
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public Task UpdateRange<T>(IEnumerable<T> entities, int updatedBy) where T : class, IAudit;

        #endregion


        #region Permamnent Delete Methods

        /// <summary>
        ///  Remove entry from database permanently
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task Delete<T>(T entity) where T : class, IAudit;

        /// <summary>
        /// Remove batch from database permanently
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task DeleteRange<T>(IEnumerable<T> entities) where T : class, IAudit;


        #endregion

        #region Soft Delete Methods (Making IsDeleted Property set to 1)

        /// <summary>
        ///  Remove entry from database softly (Making IsDeleted Property set to 1)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public Task Remove<T>(T entity, int updatedBy) where T : class, IAudit;

        /// <summary>
        /// Remove batch from database softly (Making IsDeleted Property set to 1)
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public Task RemoveBatch<T>(IEnumerable<T> entities, int updatedBy) where T : class, IAudit;


        #endregion


        #region Extensions or Miscellaneous Methods

        /// <summary>
        /// To check whether entity exists or not based on (optional) filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<bool> IsExistsAsync<T>(Expression<Func<T, bool>> filter) where T : class, IAudit;

        /// <summary>
        /// To get the data count of entity present based on (optional) filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<int> CountAsync<T>(Expression<Func<T, bool>> filter) where T : class, IAudit;

        #endregion

    }
}
