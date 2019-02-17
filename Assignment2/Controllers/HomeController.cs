using Assignment2.Models;
using Assignment2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult ADVReporting()
        {
            AdvancedVM vm = new AdvancedVM();

            //Retrieve a list of Employee ids so that it can be used to populate the dropdown on the View
            vm.Employee = GetEmployee(0);

            //Set default values for the FROM and TO dates
            vm.DateFrom = new DateTime(2014, 12, 1);
            vm.DateTo = new DateTime(2014, 12, 31);

            return View(vm);
        }

        private SelectList GetEmployee(int selected)
        {
            using (HardwareDBEntities db = new HardwareDBEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;

                //Create a SelectListItem for each Vendor record in the DB
                //Value is set to the primary key of the record and Text is set to the Name of the vendor

                var employees = db.lgemployees.Select(x => new SelectListItem

                {
                    Value = x.emp_num.ToString(),
                    Text = x.emp_fname
                    
                }).ToList();


                //If selected pearameter has a value, configure the SelectList so that the apporiate item is preselected
                if (selected == 0)
                    return new SelectList(employees, "Value", "Text");
                else
                    return new SelectList(employees, "Value", "Text", selected);
            }
        }

        //This action is used to process the Advanced report criteria entered by the user and to return report data based on that criteria
        [HttpPost]
        public ActionResult Advanced(int employee_id, string DateFrom, string DateTo)
        {
            AdvancedVM vm = new AdvancedVM {
                employee_id = employee_id,
                DateFrom = DateTime.Parse(DateFrom),
                DateTo = DateTime.Parse(DateTo)
            };
            using (HardwareDBEntities db = new HardwareDBEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;

                vm.Employee = GetEmployee(vm.employee_id);
                
                vm.employee = db.lgemployees.Where(x => x.emp_num == vm.employee_id).FirstOrDefault();

                var test = db.lginvoices.Where(i => i.employee_id == vm.employee.emp_num).Where(i => i.inv_DATETIME < vm.DateTo && i.inv_DATETIME > vm.DateFrom).ToList().Select(r => new SalesRecord
                {
                    OrderDate = (DateTime)r.inv_DATETIME,
                    Amount = Convert.ToDouble(r.inv_total),
                    Employee = vm.employee.emp_fname
                    
                    
                }).ToList();
                
                
                vm.results = test;

             
                vm.chartData = test.GroupBy(g => g.Employee).ToDictionary(g => g.Key, g => g.Sum(v => v.Amount));

                TempData["chartData"] = vm.chartData;
                TempData["records"] = test.ToList();
                TempData["employee"] = vm.employee;
                return View("ADVReporting", vm);
            }

        }

        public ActionResult EmployeeChartView()
        {
            //Load the chartData from temporary memory
            var data = TempData["chartData"];

            //Return the EmployeeOrdersChart temporary view, pass through the required chartData
            return View(TempData["chartData"]);
        }

      

       

    }
}