namespace BasicAuthenticationWebApi.Models;

public class EmployeeBL
{
	public List<Employee> GetEmployees()
	{
		List<Employee> empList = new List<Employee>();
		for (int i = 0; i < 10; i++)
		{
			if (i > 5)
			{
				empList.Add(new Employee()
				{
					Id = i,
					Name = "Name" + i,
					Dept = "IT",
					Salary = 1000 + i,
					Gender = "Male"
				});
			}
			else
			{
				empList.Add(new Employee()
				{
					Id = i,
					Name = "Name" + i,
					Dept = "HR",
					Salary = 1000 + i,
					Gender = "Female"
				});
			}
		}

		return empList;
	}
}
