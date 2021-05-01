using System;
using System.Collections.Generic;
using System.Text;

namespace MovieGateTask.DAL.Repo {
    public interface IRepo<T> where T : class {

        List<T> GetAll();
        T GetByID(object ID);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object ID);
        void save();

    }
}
