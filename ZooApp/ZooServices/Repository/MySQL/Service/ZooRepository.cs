using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using ZooServices.Repository.MySQL.Contract;

namespace ZooServices.Repository.MySQL.Service
{
    /// <summary>
    /// Implement Contract of persistence of Zoo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ZooRepository<T>:IZooRepository<T> where T:class
    {
        /// <summary>
        /// The database context zoo
        /// </summary>
        internal DbContextZoo _dbContextZoo;

        /// <summary>
        /// Initilice constructor ZooRepository
        /// </summary>
        /// <param name="dbContextZoo"></param>
        public ZooRepository(DbContextZoo dbContextZoo)
        {
            _dbContextZoo = dbContextZoo;
        }

        /// <summary>
        /// Add Entity to database
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _dbContextZoo.Set<T>().Add(entity);
        }


        /// <summary>
        /// Find by condition of Entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContextZoo.Set<T>().AsNoTracking().Where(predicate);
            return query;
        }

        /// <summary>
        /// Find one register of Entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T FindOne(Expression<Func<T, bool>> predicate)
        {
            T query = _dbContextZoo.Set<T>().AsNoTracking().SingleOrDefault(predicate);
            return query;
        }

        /// <summary>
        /// Get all data of Entity
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _dbContextZoo.Set<T>().AsNoTracking();
            return query;
        }

        /// <summary>
        /// Save data of the Entity to database
        /// </summary>
        public void SaveChanges()
        {
            _dbContextZoo.SaveChanges();
        }

        /// <summary>
        /// Update Entity to database
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            if (_dbContextZoo.Entry(entity).State == EntityState.Detached)
            {
                _dbContextZoo.Set<T>().Attach(entity);
            }
            _dbContextZoo.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes the specified object.
        /// </summary>
        /// <param name="entitie">The object.</param>
        public void Delete(T entitie)
        {
            if (_dbContextZoo.Entry(entitie).State == EntityState.Detached)
            {
                _dbContextZoo.Set<T>().Attach(entitie);
                _dbContextZoo.Set<T>().Remove(entitie);
            }
            _dbContextZoo.Entry(entitie).State = EntityState.Deleted;
        }
    }
}
