using System;
using System.Collections.Generic;
using System.Text;
using MovieGateTask.DAL.Models;
using MovieGateTask.DAL.Repo;

namespace MovietGateTask.BLL.BOs {
    public class EmployeeBO {

        private readonly IRepo <Employees> _EmployeeRepo;

        public EmployeeBO(IRepo<Employees> repo) {
            _EmployeeRepo = repo;
        }

        public IEnumerable<Employees> GetEmployees() {
            List<Employees> employees = _EmployeeRepo.GetAll();
            return employees;
        }

        public bool AddEmployee(Employees obj) {
            try {

                _EmployeeRepo.Insert(obj);
                _EmployeeRepo.save();
                return true;
            }catch(Exception e) {
                return false;
            }

        }

        public Employees GetEmployeeData(int Id) {
            return _EmployeeRepo.GetByID(Id);
        }

    }
}
