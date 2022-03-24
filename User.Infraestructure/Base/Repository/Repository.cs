using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Common.Exceptions;
using User.Infraestructure.Base.Context;
using User.Model.Base.Entity;
using User.Model.Interfaces.Base.Repository;

namespace User.Infraestructure.Base.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private UserContext _userContext;
            
        public Repository(UserContext userContext)
        {
            _userContext = userContext;
        }
        public void Add(T entity)
        {
            try
            {
                _userContext.Add(entity);
                _userContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }
        }

        public void AddRange(List<T> entities)
        {
            try
            {
                _userContext.AddRange(entities);
                _userContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }
        }

        public virtual void Delete(int id)
        {
            try
            {
                var entity = _userContext.Set<T>().FirstOrDefault(x => x.Id == id);

                if (entity != null)
                {
                    _userContext.Remove(entity);
                    _userContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }
        }

        public virtual IList<T> FindAll()
        {
            try
            {
                return _userContext.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }

        }

        public virtual T FindById(int id)
        {
            try
            {
                return _userContext.Set<T>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }

        }

        public virtual void Update(T entity)
        {
            try
            {
                _userContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _userContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }
        }

        public virtual T FindBy(Func<T, bool> predicate)
        {
            try
            {
                return _userContext.Set<T>().FirstOrDefault(predicate);
            }
            catch (Exception ex)
            {
                throw new EntityException(ex.Message, ex);
            }

        }
    }
}
