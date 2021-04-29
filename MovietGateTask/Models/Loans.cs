using System;
using System.Collections.Generic;

namespace MovietGateTask.Models {
    public partial class Loans
    {
        public Loans()
        {
            Installments = new HashSet<Installments>();
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }

        public virtual Employees Employee { get; set; }
        public virtual LoanTypes TypeNavigation { get; set; }
        public virtual ICollection<Installments> Installments { get; set; }
    }
}
