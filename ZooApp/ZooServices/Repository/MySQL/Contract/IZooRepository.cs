using System;
using System.Linq;
using System.Linq.Expressions;

namespace ZooServices.Repository.MySQL.Contract
{
    /// <summary>
    /// Definition public contract of Zoo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IZooRepository<T> where T : class
    {
        /// <summary>
        /// Get all data of Entity
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Find by condition of Entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find one register of Entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T FindOne(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Add Entity to database
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);


        /// <summary>
        /// Update Entity to database
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Save data of the Entity to database
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);
    
    }
}
