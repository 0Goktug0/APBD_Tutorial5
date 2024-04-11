using Microsoft.AspNetCore.Mvc;
using Tutorial4.Data;

namespace Tutorial4.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    // GET: api/animals
    [HttpGet]
    public ActionResult<IEnumerable<Animal>> GetAnimals()
    {
        return StaticDbContext.Animals;
    }

    // GET: api/animals/5
    [HttpGet("{id}")]
    public ActionResult<Animal> GetAnimal(int id)
    {
        var animal = StaticDbContext.Animals.FirstOrDefault(a => a.Id == id);
        if (animal == null) return NotFound();
        return animal;
    }

    // POST: api/animals
    [HttpPost]
    public ActionResult<Animal> AddAnimal(Animal animal)
    {
        StaticDbContext.Animals.Add(animal);
        return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
    }

    // PUT: api/animals/5
    [HttpPut("{id}")]
    public IActionResult UpdateAnimal(int id, Animal animal)
    {
        var existingAnimal = StaticDbContext.Animals.FirstOrDefault(a => a.Id == id);
        if (existingAnimal == null) return NotFound();

        existingAnimal.Name = animal.Name;
        // Update other fields similarly

        return NoContent();
    }

    // DELETE: api/animals/5
    [HttpDelete("{id}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animal = StaticDbContext.Animals.FirstOrDefault(a => a.Id == id);
        if (animal == null) return NotFound();

        StaticDbContext.Animals.Remove(animal);
        return NoContent();
    }
}
