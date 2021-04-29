using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovietGateTask.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace MovietGateTask.Controllers {
    [ApiController]
    //[Route("[controller]")]
    public class VacationsController : ControllerBase {

        private static List<Vacation> Vacations;
        private static Vacation DefaultTestVacation;

        private static List<Employees> Employees;
        public VacationsController() {

            Vacations = new List<Vacation>() {
                new Vacation(new DateTime(2021,3,31),new DateTime(2021,3,31),1),
                new Vacation(new DateTime(2021,4,19),new DateTime(2021,4,19),2),
                new Vacation(new DateTime(2021,4,2) ,new DateTime(2021,4,2),1),
                new Vacation(new DateTime(2021,4,1) ,new DateTime(2021,4,3),2),
                new Vacation(new DateTime(2021,4,19),new DateTime(2021,4,20),1)
            };
            DefaultTestVacation = new Vacation(new DateTime(2021 ,4 ,1) ,new DateTime(2021 ,4 ,3) ,1);

            Employees = new List<Employees>() {
                new Employees(){
                    Code="1",Name="Mustafa Gamal"
                } ,
                new Employees(){
                    Code="20"   ,Name="Ali Ahmed"
                    
                },
                new Employees(){
                    Code="2"    ,Name="Hesham Eladawy"

                },
                new Employees(){
                    Code="5"    ,Name="Ahmed Hussein"

                },
                new Employees(){
                    Code="13"   ,Name="Mustafa Hussein"

                },
                new Employees(){
                    Code="3"    ,Name="Mena Khaled"

                },
                new Employees(){
                Code="A-35" ,Name="Mahmoud Asem"
                } ,
            };
        }


        //return False if validation failed that means the vacation intersects with other vacations of the same employee otherwise returns true 
        [Route("ValidateVacation")]
        [HttpGet]
        public bool ValidateVacations() {

            Vacation Vacation = DefaultTestVacation;
            List<DateTime> VacationsOfUser = Vacations.Where(x => x.EmployeeID == Vacation.EmployeeID).Select(x=>x.Duration).Aggregate((a,b)=>{return a.Concat(b).ToList();});

            if(VacationsOfUser.Intersect(Vacation.Duration).Count() > 0)
                return false;

            return true;
        }


        //return sorted List of employees based on thier code (by numbers)
        [Route("SortEmployees")]
        [HttpGet]
        public List<Employees> SortEmployees() {
            List<Employees> SortedEmployees = new List<Employees>();

            SortedEmployees = Employees.OrderBy(x=>x.Code,new EmployeesCodeComparer()).ToList();

            return SortedEmployees;
        }

    }
    //Not internal to be resuable if needed
    public class EmployeesCodeComparer : IComparer<string> {
        public int Compare(string x ,string y) {
            var regex = new Regex("^[0-9]+");

            // run the regex on both strings
            var xRegexResult = regex.Match(x);
            var yRegexResult = regex.Match(y);

            // check if they are both numbers
            if(xRegexResult.Success && yRegexResult.Success) {
                return int.Parse(xRegexResult.Groups[0].Value).CompareTo(int.Parse(yRegexResult.Groups[0].Value));
            }

            // otherwise return as string comparison
            return x.CompareTo(y);
        }
    }

}
