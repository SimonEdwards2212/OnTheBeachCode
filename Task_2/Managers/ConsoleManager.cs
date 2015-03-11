using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class ConsoleManager
    {
        public static List<Employee> ExecuteConsole(string variable)
        {
            List<Employee> emps = new List<Employee>();
            DataSet set = new DataSet();

            if(variable.ToUpper() == "STAFF")
            {
                set = DbManager.ReturnStaff();
            }
            else
            {
                //Call DbManager to search for employee.
                set = DbManager.SearchEmployee(variable);
            }

            foreach (DataRow dr in set.Tables[0].Rows)
            {
                //Populate Employee List.
                Employee emp = Employee.MapEmployee(dr);
                emps.Add(emp);
            }
 
            return emps;
        }

        public static string FormatResults(Employee emp)
        {
            return String.Format("Name: {0}, Salary (GBP): {1}, Salary ({2}): {3}"
                                , emp.name
                                , emp.salary.Find(x => x.currency.unit == "GBP").annualAmount.ToString()
                                , emp.salary.Find(x => x.currency.unit != "GBP").currency.unit
                                , emp.salary.Find(x => x.currency.unit != "GBP").annualAmount.ToString()
                                );
        }
    }
}
