using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MovieGateTask.DAL.Models;

namespace MovieGateTask.DAL.Repo {
    public class GenericRepo<T> : IRepo<T> where T:class {

        private readonly TaskContext _DB;
        private readonly DbSet<T> _table;

        public GenericRepo(TaskContext db) {
            _DB = db;
            _table = _DB.Set<T>(); 
        }

        public List<T> GetAll() {
            return _table.ToList();
        }

        public T GetByID(object ID) {
            return _table.Find(ID);
        }

        public void Insert(T obj) {
            _table.Add(obj);
        }

        public void Update(T obj) {
            _table.Attach(obj);
            _DB.Entry(obj).State = EntityState.Modified; 
        }

        public void Delete(object ID) {
            T exists = _table.Find(ID);
            _table.Remove(exists);
        }

        public void save() {
            _DB.SaveChanges();
        }
    }
}
