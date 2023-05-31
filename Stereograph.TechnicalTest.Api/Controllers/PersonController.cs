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

        return CreatedAtAction(nameof(GetPerson), new { id = p_Person.Id }, p_Person);
    }
}
