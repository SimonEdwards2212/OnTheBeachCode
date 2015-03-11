SELECT e.name
	 , CONVERT(DECIMAL(18,2),s.annual_amount / c.conversion_factor)
  FROM dbo.Employees e
 INNER JOIN
	   dbo.Salaries s
    ON e.id = s.employee_id
 INNER JOIN
	   dbo.Currencies c
	ON c.id = s.currency