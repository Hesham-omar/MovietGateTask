using System;
using System.Collections.Generic;
using System.Text;
using MovieGateTask.DAL.Models;
using MovieGateTask.DAL.Repo;
using System.Linq;

namespace MovietGateTask.BLL.BOs {
    public class EmployeeBO {

        private readonly IRepo<Employees> _EmployeeRepo;

        public EmployeeBO(IRepo<Employees> repo) {
            _EmployeeRepo = repo;
        }

        public IEnumerable<Employees> GetEmployees() {
            List<Employees> employees =  _EmployeeRepo.Get(x=>x.Id>0,new List<string>()).ToList();
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
            List<string> includes = new List<string>() {
                    "Loans","Loans.Installments"
                };
            return  _EmployeeRepo.Get(x=>x.Id>0,includes).First();
        }

    }
}
