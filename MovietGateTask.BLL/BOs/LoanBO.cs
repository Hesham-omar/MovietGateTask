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

        public LoanBO(IRepo<Loans> loanRepo ,IRepo<Employees> employeesRepo ) {
            _loanRepo = loanRepo;
            _EmployeesRepo = employeesRepo;
        }

        public bool CheckEmployeeCurrentLoans(int employeeID) {
            try {
                List<string> includes = new List<string>() { 
                    "Loans","Loans.Installments"
                };

                Employees emp =  _EmployeesRepo.Get(x=>x.Id == employeeID,includes).First();
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

        public bool AddNewLoan(Loans loan , int installmentCount ,List<DateTime> excepted) {

            try {
                _loanRepo.Insert(loan);

                List<DateTime> installmentsMonthes = new List<DateTime>();
                DateTime now = loan.StartDate;
                int diff = ((loan.EndDate.Year - now.Year) * 12) + loan.EndDate.Month - now.Month + 1;

                while(diff>0) {
                    installmentsMonthes.Add(now);
                    now = now.AddMonths(1);
                    diff --;
                }
                
                foreach(var date in excepted) {
                    installmentsMonthes.Remove(date);
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
            List<string> includes = new List<string>() { 
                "Installments"
            };
            return _loanRepo.Get(x => x.Id == Id ,includes).FirstOrDefault();
        }
    }
}
