namespace BasicAuthenticationWebApi.Models;

public class Employee
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string Gender { get; set; } = null!;
	public string Dept { get; set; } = null!;
	public int Salary { get; set; }
}
