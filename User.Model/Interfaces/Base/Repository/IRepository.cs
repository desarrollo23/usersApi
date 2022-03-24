using System;
using System.Collections.Generic;

namespace User.Model.Interfaces.Base.Repository
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void AddRange(List<T> entity);
        IList<T> FindAll();

        void Delete(int id);

        T FindById(int id);

        void Update(T entity);

        T FindBy(Func<T, bool> predicate);
    }
}
