using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovietGateTask.Models;
using Newtonsoft.Json;
using MovietGateTask.ViewModels;
using Microsoft.EntityFrameworkCore;
using MovieGateTask.DAL.Models;

namespace MovietGateTask.Controllers {
    [ApiController]
    public class LoansController : Controller {
        private readonly TaskContext DB;

        public LoansController(TaskContext db) {
            DB = db;
        }


        [Route("Loans")]
        public IActionResult Loans() {
            List<Employees> employees = DB.Employees.ToList();
            List<LoanTypes> loanTypes = DB.LoanTypes.ToList();

            LoansViewModel Data = new LoansViewModel() {
                Employees = employees ,
                LoanTypes = loanTypes
            };

            return View(Data);
        }


        [Route("addEmployee")]
        [HttpPost]
        public int addemployee([FromForm] Employees employee) {

            try {
                DB.Employees.Add(employee);
                DB.SaveChanges();
                return employee.Id;

            } catch(Exception e) {
                return -1;
            }

        }

        [HttpPost]
        [Route("addLoan")]
        public int AddLoan([FromForm] Loans Loan ,[FromForm] short installmentCount) {
            try {
                Employees employee = DB.Employees.Include("Loans").Include("Loans.Installments").FirstOrDefault(x => x.Id == Loan.EmployeeId);

                if(employee != null) {
                    Loans installments = null;
                    if(employee.Loans != null && employee.Loans.Count > 0)
                        installments = employee.Loans.Where(x => x.Installments.OrderByDescending(x => x.Date).FirstOrDefault().Date > DateTime.Now).FirstOrDefault();

                    if(installments == null) {
                        DB.Loans.Add(Loan);

                        List<DateTime> installmentsMonthes = new List<DateTime>();
                        DateTime now = Loan.StartDate;
                        while(now <= Loan.EndDate) {
                            installmentsMonthes.Add(now);
                            now = now.AddMonths(1);
                        }
                        var installmentAmount = Loan.TotalAmount / installmentCount;

                        for(int i = 0; i < installmentCount - 1; i++) {

                            Loan.Installments.Add(new Installments() {
                                Amount = installmentAmount ,
                                Date = installmentsMonthes[i]
                            });
                        }

                        DB.SaveChanges();
                        return Loan.Id;
                    }
                    return -1;
                }
                return -2;
            } catch(Exception e) {
                return -3;
            }
        }


        [HttpGet("LoanID")]
        [Route("GetLoanData")]
        public Loans GetLoanData(int LoanID) {
            Loans loan = DB.Loans.FirstOrDefault(x => x.Id == LoanID);
            return loan;
        }

        [HttpGet("EmployeeID")]
        [Route("GetEmployeeData")]
        public Employees GetEmployeeData(int EmployeeID) {
            Employees emp = DB.Employees.FirstOrDefault(x => x.Id == EmployeeID);
            return emp;
        }

    }
}
