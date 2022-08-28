using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace SuperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero { Id = 1, Name ="Spider Man",firstName ="Peter",lastName ="Parker",place = "New York"},
                new SuperHero { Id = 2, Name ="Tony Stark",firstName ="Tony",lastName ="Stark",place = "London"},
            };
        private readonly DataContext _context;  
        public SuperHeroController(DataContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }
            dbHero.Name = request.Name;
            dbHero.place = request.place;
            dbHero.firstName = request.firstName;
            dbHero.lastName = request.lastName;
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            /*var hero = heroes.Find(h => h.Id == id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }*/
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }
            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
