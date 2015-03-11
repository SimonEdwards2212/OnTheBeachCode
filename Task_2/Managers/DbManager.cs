using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class DbManager
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
        private static readonly string dataProvider = System.Configuration.ConfigurationManager.ConnectionStrings["Db"].ProviderName;
        private static readonly DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);

        public static DataSet SearchEmployee(string name)
        {
            string sql = @"SELECT e.name AS Name 
	                         , c.unit AS LocalCurrency
	                         , s.annual_amount AS Salary
	                         , CONVERT(DECIMAL(18,2),s.annual_amount / c.conversion_factor) AS SalaryGBP
                          FROM dbo.Employees e
                         INNER JOIN
	                           dbo.Salaries s
                            ON e.id = s.employee_id
                         INNER JOIN
	                           dbo.Currencies c
	                        ON c.id = s.currency
                         WHERE e.name = @name";

            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@name", name);

            return GetDataSet(sql, sqlParam);
        }

        public static DataSet ReturnStaff()
        {
            string sql = @"SELECT e.name AS Name
	                         , c.unit AS LocalCurrency
	                         , s.annual_amount AS Salary
	                         , CONVERT(DECIMAL(18,2),s.annual_amount / c.conversion_factor) AS SalaryGBP
                          FROM dbo.Employees e
                         INNER JOIN
	                           dbo.Roles r
	                        ON e.role_id = r.id
                         INNER JOIN
	                           dbo.Salaries s
                            ON e.id = s.employee_id
                         INNER JOIN
	                           dbo.Currencies c
	                        ON c.id = s.currency
                         WHERE r.name = 'STAFF'";

            return GetDataSet(sql, null);
        }

        private static DataSet GetDataSet(string sql, SqlParameter[] arrParam)
        {
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;

                    if (arrParam != null)
                    {
                        foreach (SqlParameter sqlParameter in arrParam)
                        {
                            command.Parameters.Add(sqlParameter);
                        }
                    }
                    connection.Open();

                    using (DbDataAdapter adapter = factory.CreateDataAdapter())
                    {
                        adapter.SelectCommand = command;

                        var ds = new DataSet();
                        adapter.Fill(ds);

                        return ds;
                    }
                }
            }
        }
    }
}
