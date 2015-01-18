using MD.TODO.Data.Context;
using MD.TODO.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MD.TODO.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> Table { get; }
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IDbContext _context;

        private IDbSet<TEntity> _entities;

        private IDbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<TEntity>();
                }
                return _entities;
            }
        }

        public Repository(IDbContext context)
        {
            _context = context;
        }

        public TEntity GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public void Insert(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Add(entity);
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Remove(entity);
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public virtual IQueryable<TEntity> Table
        {
            get { return Entities; }
        }
    }
}
