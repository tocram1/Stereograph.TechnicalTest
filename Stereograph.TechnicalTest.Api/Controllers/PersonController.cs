using Microsoft.AspNetCore.Mvc;
using Stereograph.TechnicalTest.Api.Entities;
using Stereograph.TechnicalTest.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Stereograph.TechnicalTest.Api.Controllers;

[Route("api/people")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly ApplicationDbContext m_DbContext;

    public PersonController(ApplicationDbContext p_DbContext)
    {
        m_DbContext = p_DbContext;
    }

    // GET api/people
    [HttpGet]
    public ActionResult<IEnumerable<Person>> GetPeople()
    {
        List<Person> v_People = m_DbContext.People.ToList();
        return Ok(v_People);
    }

    // GET api/people/{p_Id}
    [HttpGet("{p_Id}")]
    public ActionResult<Person> GetPerson(int p_Id)
    {
        Person v_Person = m_DbContext.People.Find(p_Id);

        if (v_Person is null)
            return NotFound();

        return Ok(v_Person);
    }

    // POST api/people
    [HttpPost]
    public ActionResult<Person> CreatePerson(Person p_Person)
    {
        m_DbContext.People.Add(p_Person);
        m_DbContext.SaveChanges();

        return CreatedAtAction(nameof(GetPerson), new { p_Id = p_Person.Id }, p_Person);
    }


    // PUT api/people/{p_Id}
    [HttpPut("{p_Id}")]
    public ActionResult UpdatePerson(int p_Id, Person p_UpdatedPerson)
    {
        Person v_Person = m_DbContext.People.Find(p_Id);

        if (v_Person is null)
            return NotFound();

        v_Person.FirstName = p_UpdatedPerson.FirstName;
        v_Person.LastName = p_UpdatedPerson.LastName;
        v_Person.Email = p_UpdatedPerson.Email;
        v_Person.Address = p_UpdatedPerson.Address;
        v_Person.City = p_UpdatedPerson.City;

        m_DbContext.SaveChanges();

        return NoContent();
    }

    // DELETE api/people/{p_Id}
    [HttpDelete("{p_Id}")]
    public ActionResult DeletePerson(int p_Id)
    {
        Person v_Person = m_DbContext.People.Find(p_Id);

        if (v_Person is null)
            return NotFound();

        m_DbContext.People.Remove(v_Person);
        m_DbContext.SaveChanges();

        return NoContent();
    }

}
