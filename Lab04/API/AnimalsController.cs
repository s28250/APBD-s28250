using Microsoft.AspNetCore.Mvc;
using SampleAPIControllers.Models;


namespace SampleAPIControllers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private static readonly List<Animal> _animals = new()
    {
        new Animal { Id = 1, Name = "Charlie", Category = "Dog", Weight = 23.5f, FurColor = "Black" },
        new Animal { Id = 2, Name = "Max", Category = "Cat", Weight = 5.2f, FurColor = "White" },
        new Animal { Id = 3, Name = "Buddy", Category = "Dog", Weight = 30.0f, FurColor = "Brown" }
    };

    [HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok(_animals);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal(int id)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }
        return Ok(animal);
    }

    [HttpPost]
    public IActionResult CreateAnimal(Animal animal)
    {
        animal.Id = _animals.Max(a => a.Id) + 1;
        _animals.Add(animal);
        return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, Animal updatedAnimal)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }
        animal.Name = updatedAnimal.Name;
        animal.Category = updatedAnimal.Category;
        animal.Weight = updatedAnimal.Weight;
        animal.FurColor = updatedAnimal.FurColor;
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }
        _animals.Remove(animal);
        return NoContent();
    }
}