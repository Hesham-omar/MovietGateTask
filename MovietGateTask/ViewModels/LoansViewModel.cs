using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieGateTask.DAL.Models;
using MovietGateTask.Models;

namespace MovietGateTask.ViewModels {
    public class LoansViewModel {

        public IEnumerable<Employees> Employees { get; set; }
        public IEnumerable<LoanTypes> LoanTypes { get; set; }

    }
}
