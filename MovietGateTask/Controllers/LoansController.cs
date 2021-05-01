using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovietGateTask.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using MovieGateTask.DAL.Models;
using MovietGateTask.BLL.BOs;
using MovietGateTask.EP.ViewModels;

namespace MovietGateTask.Controllers {
    [ApiController]
    public class LoansController : Controller {
        private readonly EmployeeBO _employeeBo;
        private readonly LoanBO _loanBO;
        private readonly LoanTypeBO _loanTypesBO;

        public LoansController(EmployeeBO employeeBo ,LoanBO loanBO ,LoanTypeBO loanTypesBO) {
            _employeeBo = employeeBo;
            _loanBO = loanBO;
            _loanTypesBO = loanTypesBO;
        }


        [Route("Loans")]
        public IActionResult Loans() {
            var data = new LoansViewModel() {
                Employees = _employeeBo.GetEmployees() ,
                LoanTypes = _loanTypesBO.GetTypes(),
            };

            return View(data);
        }


        [Route("addEmployee")]
        [HttpPost]
        public int addemployee([FromForm] Employees employee) {

            bool inserted = _employeeBo.AddEmployee(employee);

            if(inserted)
                return employee.Id;
            return -1;
        }

        [HttpPost]
        [Route("addLoan")]
        public int AddLoan([FromForm] Loans Loan ,[FromForm] short installmentCount,[FromForm] string[] ExceptedMonths) {
            try {
                List<DateTime> Excepted = new List<DateTime>();
                foreach(var date in ExceptedMonths) {
                    string[] data = date.Split('-');
                    Excepted.Add(new DateTime(int.Parse(data[0]),int.Parse(data[1]),1));
                }

                if(Loan.Notes == null)
                    Loan.Notes = "";

                int  diff = ((Loan.EndDate.Year - Loan.StartDate.Year) * 12) + Loan.EndDate.Month - Loan.StartDate.Month +1;
                if(!(Loan.StartDate <= DateTime.Now || diff < 1 || installmentCount + Excepted.Count() != diff)) {
                    if(_loanBO.CheckEmployeeCurrentLoans(Loan.EmployeeId)) {
                        if(_loanBO.AddNewLoan(Loan ,installmentCount ,Excepted)) {
                            return Loan.Id;
                        } else {
                            return -1;
                        }
                    }
                        return -2;
                }
                return -3;
            } catch(Exception e) {
                return -4;
            }
        }


        [HttpGet("LoanID")]
        [Route("GetLoanData")]
        public IActionResult GetLoanData(int LoanID) {
            Loans loan = _loanBO.GetLoanData(LoanID);
            ObjectResult result = new ObjectResult(loan) {
                Value = loan,StatusCode=200
            };
            return result;
        }

        [HttpGet("EmployeeID")]
        [Route("GetEmployeeData")]
        public Employees GetEmployeeData(int EmployeeID) {
            Employees emp = _employeeBo.GetEmployeeData(EmployeeID);
            ObjectResult result = new ObjectResult(emp) {
                Value = emp ,
                StatusCode = 200
            };
            return emp;
        }

    }
}
