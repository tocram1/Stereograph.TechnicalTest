using CsvHelper.Configuration;

namespace Stereograph.TechnicalTest.Api.Models;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
}

public sealed class PersonMap : ClassMap<Person>
{
    public PersonMap()
    {
        Map(m => m.FirstName).Name("first_name");
        Map(m => m.LastName).Name("last_name");
        Map(m => m.Email).Name("email");
        Map(m => m.Address).Name("address");
        Map(m => m.City).Name("city");
    }
}