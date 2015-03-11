using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Employee
    {
        #region properties

        public int id { get; set; }
        public string name { get; set; }

        public Role role { get; set; }

        public List<Salary> salary { get; set; }

        #endregion

        public static Employee MapEmployee(DataRow data)
        {
            Employee emp = new Employee();

            emp.name = data["Name"].ToString();
            emp.salary = new List<Salary>();

            Salary sal = new Salary();
            sal.annualAmount = Convert.ToDecimal(data["Salary"]);
            sal.currency = new Currency();
            sal.currency.unit = data["LocalCurrency"].ToString();

            emp.salary.Add(sal);

            Salary sal1 = new Salary();
            sal1.annualAmount = Convert.ToDecimal(data["SalaryGBP"]);
            sal1.currency = new Currency();
            sal1.currency.unit = "GBP";

            emp.salary.Add(sal1);

            return emp;
        }
    }
}
