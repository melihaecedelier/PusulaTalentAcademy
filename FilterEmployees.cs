using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class FilterEmployees
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        if (employees == null || !employees.Any())
        {
            return JsonSerializer.Serialize(new
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MinSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }

        var filteredEmployees = employees
            .Where(e => e.Age >= 25 && e.Age <= 40)
            .Where(e => e.Department == "IT" || e.Department == "Finance")
            .Where(e => e.Salary >= 5000 && e.Salary <= 9000)
            .Where(e => e.HireDate.Year > 2017)
            .ToList();

        List<string> sortedNames = filteredEmployees
            .OrderByDescending(e => e.Name.Length)
            .ThenBy(e => e.Name)
            .Select(e => e.Name)
            .ToList();

        int count = filteredEmployees.Count;

        decimal totalSalary = count > 0 ? filteredEmployees.Sum(e => e.Salary) : 0;
        decimal averageSalary = count > 0 ? filteredEmployees.Average(e => e.Salary) : 0;
        decimal minSalary = count > 0 ? filteredEmployees.Min(e => e.Salary) : 0;
        decimal maxSalary = count > 0 ? filteredEmployees.Max(e => e.Salary) : 0;

        var result = new
        {
            Names = sortedNames,
            TotalSalary = totalSalary,
            AverageSalary = Math.Round(averageSalary, 2),
            MinSalary = minSalary,
            MaxSalary = maxSalary,
            Count = count
        };

        return JsonSerializer.Serialize(result);
    }
}
