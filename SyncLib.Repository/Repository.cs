using Microsoft.EntityFrameworkCore;
using SyncLib.Repository.Database;
using SyncLib.Repository.Interface;
using System.Linq.Expressions;

namespace SyncLib.Repository
{
    public class Repository : IRepository
    {
        private readonly SyncLib_DbContext _context;
        public Repository(SyncLib_DbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Save all the changes to database ***Mandatory to call this function after every transaction.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        #region Get Methods

        /// <summary>
        ///  Get all data list of entity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, IAudit
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Get all data list of entity based on given condition.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> filter) where T : class, IAudit
        {
            return await _context.Set<T>()
                .Where(filter)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Get all data list of entity based on given condition including joint properties.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties) where T : class, IAudit
        {
            var nonDeleted = _context.Set<T>()
                .Where(filter);

            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                    await nonDeleted.Include(property).LoadAsync();
            }

            return await nonDeleted.ToListAsync();

        }


        /// <summary>
        /// Get Entity based on given condition
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(Expression<Func<T, bool>> filter) where T : class, IAudit
        {
            return await _context.Set<T>().Where(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get Entity based on given condition including joint properties.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties) where T : class, IAudit
        {
            foreach (var property in includeProperties)
                await _context.Set<T>().Include(property).LoadAsync();

            return await _context.Set<T>().FirstOrDefaultAsync(filter);
        }

        #endregion


        #region Add Methods

        /// <summary>
        /// Add new entry
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public async Task AddAsync<T>(T entity, int createdBy) where T : class, IAudit
        {
            entity.CreatedOn = DateTime.Now;
            entity.CreatedBy = createdBy;
            entity.UpdatedOn = DateTime.Now;
            entity.UpdatedBy = createdBy;
            entity.IsDeleted = false;

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add new batch entry
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public async Task AddRangeAsync<T>(IEnumerable<T> entities, int createdBy) where T : class, IAudit
        {
            foreach (var entity in entities)
            {
                entity.CreatedOn = DateTime.Now;
                entity.CreatedBy = createdBy;
                entity.UpdatedOn = DateTime.Now;
                entity.UpdatedBy = createdBy;
                entity.IsDeleted = false;
            }
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        #endregion


        #region Update Methods

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updatedBy"></param>
        public async Task Update<T>(T entity, int updatedBy) where T : class, IAudit
        {
            entity.UpdatedOn = DateTime.Now;
            entity.UpdatedBy = updatedBy;
            entity.IsDeleted = false;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Update batch at once
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public async Task UpdateRange<T>(IEnumerable<T> entities, int updatedBy) where T : class, IAudit
        {
            foreach (var entity in entities)
            {
                entity.UpdatedOn = DateTime.Now;
                entity.UpdatedBy = updatedBy;
                entity.IsDeleted = false;
            }
            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        #endregion


        #region Permanent Delete Methods

        /// <summary>
        ///  Remove entry from database permanently
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Delete<T>(T entity) where T : class, IAudit
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Remove batch from database permanently
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task DeleteRange<T>(IEnumerable<T> entities) where T : class, IAudit
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();

        }

        #endregion

        #region Soft Delete Methods (Making IsDeleted Property set to 1)

        /// <summary>
        ///  Remove entry from database softly (Making IsDeleted Property set to 1)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public async Task Remove<T>(T entity, int updatedBy) where T : class, IAudit
        {
            entity.UpdatedOn = DateTime.Now;
            entity.UpdatedBy = updatedBy;
            entity.IsDeleted = true;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Remove batch from database softly (Making IsDeleted Property set to 1)
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public async Task RemoveBatch<T>(IEnumerable<T> entities, int updatedBy) where T : class, IAudit
        {

            foreach (var entity in entities)
            {
                entity.UpdatedOn = DateTime.Now;
                entity.UpdatedBy = updatedBy;
                entity.IsDeleted = true;
            }
            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }


        #endregion

        #region Extensions or Miscellaneous Methods
        /// <summary>
        /// To check whether entity exists or not based on (optional) filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<bool> IsExistsAsync<T>(Expression<Func<T, bool>> filter) where T : class, IAudit
        {
            return await _context.Set<T>().Where(filter).AnyAsync();
        }

        /// <summary>
        /// To get the data count of entity present based on (optional) filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<int> CountAsync<T>(Expression<Func<T, bool>> filter) where T : class, IAudit
        {
            return await _context.Set<T>().Where(filter).CountAsync();
        }

        #endregion
    }
}