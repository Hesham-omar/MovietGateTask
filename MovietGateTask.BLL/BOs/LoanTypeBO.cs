using MovieGateTask.DAL.Models;
using MovieGateTask.DAL.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovietGateTask.BLL.BOs {
    public class LoanTypeBO {
        private readonly IRepo<LoanTypes> _loanTypes;

        public LoanTypeBO(IRepo<LoanTypes> loanTypes) {
            _loanTypes = loanTypes;
        }

        public IEnumerable<LoanTypes> GetTypes() {
            List<LoanTypes> types = (List<LoanTypes>) _loanTypes.Get(x => x.Id > 0 ,new List<string>());
            return types;
        }

    }
}
