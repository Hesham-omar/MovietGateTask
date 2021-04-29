using System;
using System.Collections.Generic;

namespace MovietGateTask.Models {
    public partial class LoanTypes
    {
        public LoanTypes()
        {
            Loans = new HashSet<Loans>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Loans> Loans { get; set; }
    }
}
