using System;
using System.Collections.Generic;

namespace MovieGateTask.DAL.Models
{
    public partial class Employees
    {
        public Employees()
        {
            Loans = new HashSet<Loans>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Loans> Loans { get; set; }
    }
}
