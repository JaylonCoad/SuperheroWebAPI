using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperheroWebAPI.Data;
using SuperheroWebAPI.Entities;

namespace SuperheroWebAPI.Controllers
{
    [Route("api/[controller]")] // creating a new API endpoint
    [ApiController]
    public class SuperheroController : ControllerBase
    {
        private readonly DataContext _context; // dependency injection: taking in information from a different file and
                                               // injecting it into current file for its use
        public SuperheroController(DataContext context) // constructor
        {
            _context = context;
        }

        [HttpGet] // get request method which lists out all the heroes from the DB
        public async Task<ActionResult<List<Superhero>>> GetAllHeroes()
        {
            var heroes = await _context.superheroes.ToListAsync(); // list the heroes from the database
            return Ok(heroes); // returns the status code 200, 404 means not found
        }


        [HttpGet("{id}")] // get request method which gets a specific hero using an ID
        public async Task<ActionResult<Superhero>> GetHero(int id)
        {
            var hero = await _context.superheroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found.");
            }
            return Ok(hero); // returns the status code 200, 404 means not found
        }

        [HttpPost] // post request method which creates a new superhero and puts into DB
        public async Task<ActionResult<List<Superhero>>> AddHero(Superhero hero)
        {
            _context.superheroes.Add(hero); // adding a hero to DB
            await _context.SaveChangesAsync(); // save changes to DB
            return Ok(await _context.superheroes.ToListAsync()); // returns the list of heroes after adding a new hero
        }

        [HttpPut] // put method which will update a property of the superhero object given a superhero object
        public async Task<ActionResult<List<Superhero>>> UpdateHero(Superhero updatedHero)
        {
            var dbHero = await _context.superheroes.FindAsync(updatedHero.Id);
            if (dbHero == null) // meaning that the hero wasn't found
            {
                return BadRequest("Hero not found.");
            }
            dbHero.Name = updatedHero.Name;
            dbHero.FirstName = updatedHero.FirstName;
            dbHero.LastName = updatedHero.LastName;
            dbHero.Place = updatedHero.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.superheroes.ToListAsync()); // returns the list of heroes after adding a new hero
        }

        [HttpDelete] // deleting a hero from DB given an id
        public async Task<ActionResult<Superhero>> DeleteHero(int id)
        {
            var dbHero = await _context.superheroes.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found.");
            }
            _context.superheroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.superheroes.ToListAsync()); // returns the status code 200, 404 means not found
        }
    }
}
