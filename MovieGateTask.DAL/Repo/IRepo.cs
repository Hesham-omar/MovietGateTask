using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MovieGateTask.DAL.Repo {
    public interface IRepo<T> where T : class {

        IEnumerable<T> Get(Expression<Func<T ,bool>> predicate ,List<string>Includes);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object ID);
        void save();

    }
}
