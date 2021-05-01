using System;
using System.Collections.Generic;
using System.Text;
using MovieGateTask.DAL.Repo;
using MovieGateTask.DAL.Models;
using System.Linq;

namespace MovietGateTask.BLL.BOs {
    public class LoanBO {
        private readonly IRepo<Loans> _loanRepo;
        private readonly IRepo<Employees> _EmployeesRepo;

        public LoanBO(IRepo<Loans> loanRepo ,IRepo<Employees> employeesRepo) {
            _loanRepo = loanRepo;
            _EmployeesRepo = employeesRepo;
        }

        public bool CheckEmployeeCurrentLoans(int employeeID) {
            try {
                Employees emp =  _EmployeesRepo.GetByID(employeeID);
                if(emp == null)
                    return false;
                Loans installments = null;

                if(emp.Loans!= null && emp.Loans.Count > 0) {
                    installments = emp.Loans.Where(x => x.Installments.OrderByDescending(x => x.Date).FirstOrDefault().Date > DateTime.Now).FirstOrDefault();
                    if(installments == null)
                        return true;
                    return false;
                }

                return true;
            } catch(Exception e) { return false; }
        }

        public bool AddNewLoan(Loans loan , int installmentCount) {

            try {
                _loanRepo.Insert(loan);

                List<DateTime> installmentsMonthes = new List<DateTime>();
                DateTime now = loan.StartDate;
                while(now <= loan.EndDate) {
                    installmentsMonthes.Add(now);
                    now = now.AddMonths(1);
                }
                var installmentAmount = loan.TotalAmount / installmentCount;

                for(int i = 0; i < installmentCount - 1; i++) {

                    loan.Installments.Add(new Installments() {
                        Amount = installmentAmount ,
                        Date = installmentsMonthes[i]
                    });
                }
                _loanRepo.save();

                return true;
            }catch(Exception e) {
                return false;
            }

        }

        public Loans GetLoanData(int Id) {
            return _loanRepo.GetByID(Id);
        }
    }
}
