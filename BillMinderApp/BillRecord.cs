using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillMinderApp
{
    public class BillRecord
    {
        public string BillName { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
        public string DueDate { get; set; }
        public string Repeat { get; set; }
        public string Note { get; set; }
    }
}
