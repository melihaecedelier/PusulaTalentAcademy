using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Collections.Generic;

public class FilterPeopleFromXml
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        string fullXml = $"<People>{xmlData}</People>";

        XDocument doc;
        try
        {
            doc = XDocument.Parse(fullXml);
        }
        catch (System.Xml.XmlException)
        {
            return JsonSerializer.Serialize(new 
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }

        var filteredPeople = doc.Descendants("Person")
            .Where(p =>
                int.Parse(p.Element("Age")?.Value ?? "0") > 30 &&
                p.Element("Department")?.Value == "IT" &&
                decimal.Parse(p.Element("Salary")?.Value ?? "0") > 5000 &&
                DateTime.Parse(p.Element("HireDate")?.Value ?? "2020-01-01").Year < 2019
            )
            .Select(p => new 
            {
                Name = p.Element("Name")?.Value,
                Salary = decimal.Parse(p.Element("Salary")?.Value ?? "0")
            })
            .ToList();
            
        decimal totalSalary = filteredPeople.Sum(p => p.Salary);
        int count = filteredPeople.Count;
        decimal averageSalary = count > 0 ? totalSalary / count : 0;
        decimal maxSalary = count > 0 ? filteredPeople.Max(p => p.Salary) : 0;
        List<string> names = filteredPeople.Select(p => p.Name).OrderBy(n => n).ToList();

        var result = new 
        {
            Names = names,
            TotalSalary = totalSalary,
            AverageSalary = averageSalary,
            MaxSalary = maxSalary,
            Count = count
        };

        return JsonSerializer.Serialize(result);
    }
}
