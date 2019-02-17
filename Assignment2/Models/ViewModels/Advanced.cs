using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Models.ViewModels
{
    public class AdvancedVM
    {
        //Fields for report criteria
        public IEnumerable<SelectListItem> Employee { get; set; }
        public int employee_id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        //Fields for report data
        public lgemployee employee { get; set; }
        public List<SalesRecord> results { get; set; }
        public Dictionary<string, double> chartData { get; set; }
    }

    public class SalesRecord
    {
        public DateTime OrderDate { get; set; }
        public double Amount { get; set; }
        public string Employee { get; set; }
        
    }
}