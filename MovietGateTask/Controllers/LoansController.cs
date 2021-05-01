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
        public int AddLoan([FromForm] Loans Loan ,[FromForm] short installmentCount) {
            try {

                if(_loanBO.CheckEmployeeCurrentLoans(Loan.EmployeeId)) {

                    if(_loanBO.AddNewLoan(Loan,installmentCount))
                        return Loan.Id;

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
            Loans loan = _loanBO.GetLoanData(LoanID);
            return loan;
        }

        [HttpGet("EmployeeID")]
        [Route("GetEmployeeData")]
        public Employees GetEmployeeData(int EmployeeID) {
            Employees emp = _employeeBo.GetEmployeeData(EmployeeID);
            return emp;
        }

    }
}
