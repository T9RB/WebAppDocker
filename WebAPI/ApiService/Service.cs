using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.ApiService;

public class Service
{
    private AppDbContext _dbcontext;

    public Service(AppDbContext context)
    {
        _dbcontext = context;
    }

    public List<Person> GetAllPersons()
    {
        var persons = _dbcontext.persons.Include(x => x.skills).ToList();

        var personsList = persons.Select(x => new Person()
        {
                id = x.id,
                name = x.name,
                displayName = x.displayName,
                skills = x.skills.Select(c => new Skills() {name = c.name, level = c.level}).ToList()
        }).ToList();
        
        return personsList;
    }

    public Person GetPerson(long idPerson)
    {
        var person = _dbcontext.persons.Include(x => x.skills).FirstOrDefault(x => x.id == idPerson);

        try
        {
            var findPerson = new Person()
            {
                id = person.id,
                name = person.name,
                displayName = person.displayName,
                skills = person.skills.Select(c => new Skills() {name = c.name, level = c.level}).ToList()
            };
            return findPerson;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("The person was not found");
        }
    }

    public void PostNewPerson(PersonForResponse personFromReq)
    {
        try
        {
            var newPerson = new Person()
            {
                name = personFromReq.name,
                displayName = personFromReq.name,
                skills = personFromReq.skills.Select(x => new Skills() {name = x.name, level = x.level}).ToList()
            };

            _dbcontext.persons.Add(newPerson);
            _dbcontext.SaveChanges();
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Error filling in parameters");
        }
    }

    public void PutPerson(long id, Person personFromReq)
    {
        Person findPerson = _dbcontext.persons.FirstOrDefault(x => x.id == id);

        try
        {
            findPerson.name = personFromReq.name;
            findPerson.displayName = personFromReq.displayName;

            var findSkills = personFromReq.skills.Select(c => new Skills() {name = c.name, level = c.level}).ToList();
            findPerson.skills = findSkills;
            _dbcontext.SaveChanges();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("The person was not found");
        }
    }

    public void DeletePerson(long id)
    {
        var findForDeletePerson = GetPerson(id);

        if (findForDeletePerson != null)
        {
            _dbcontext.persons.Remove(findForDeletePerson);
            _dbcontext.SaveChanges();
        }
        else
        {
            throw new InvalidOperationException("The person was not deleted!");
        }
    }
}