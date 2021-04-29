using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovietGateTask.Models {
    public class Vacation {
        public int EmployeeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [JsonIgnore]
        public List<DateTime> Duration { get; set; }

        public Vacation(DateTime start , DateTime end,int emplyeeid) {
            EmployeeID = emplyeeid;
            StartDate = start;
            EndDate = end;
            CaluclateDuration();
        }

        public void CaluclateDuration() {
            Duration = Enumerable.Range(0 ,1 + EndDate.Subtract(StartDate).Days)
                      .Select(offset => StartDate.AddDays(offset))
                      .ToList();

        }
    }
}
