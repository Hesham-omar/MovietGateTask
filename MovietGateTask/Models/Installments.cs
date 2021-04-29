using System;
using System.Collections.Generic;

namespace MovietGateTask.Models {
    public partial class Installments
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }

        public virtual Loans Loan { get; set; }
    }
}
