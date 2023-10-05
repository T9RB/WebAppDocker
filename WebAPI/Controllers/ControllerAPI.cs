using Microsoft.AspNetCore.Mvc;
using WebAPI.ApiService;
using WebAPI.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/")]
public class ControllerAPI : ControllerBase
{
    private Service _service;

    public ControllerAPI(Service _apiService)
    {
        _service = _apiService;
    }
    
    [HttpGet]
    [Route("persons")]
    public ActionResult<List<Person>> Get()
    {
        return _service.GetAllPersons();
    }

    [HttpGet]
    [Route("findperson={id}")]
    public ActionResult<Person> GetPerson(long id)
    {
        return _service.GetPerson(id);
    }

    [HttpPost]
    [Route("newperson")]
    public IActionResult PostPerson(PersonForResponse personFromReq)
    {
        _service.PostNewPerson(personFromReq);
        return Ok();
    }

    [HttpPut]
    [Route("updateperson/id={id}")]
    public IActionResult PutPerson(long id, Person personReq)
    {
        _service.PutPerson(id, personReq);
        return Ok();
    }
    
    [HttpDelete]
    [Route("deleteperson")]
    public IActionResult DeletePerson(long id, Person personReq)
    {
        _service.DeletePerson(id);
        return Ok();
    }
}