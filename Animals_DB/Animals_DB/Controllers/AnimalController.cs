using Animals_DB.Models;
using Animals_DB.Services;
using Microsoft.AspNetCore.Mvc;

namespace Animals_DB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalController : ControllerBase
{
    private readonly IAnimalService _animalService;

    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    [HttpGet]
    public IActionResult GetAnimals([FromQuery] string orderBy = "name")
    {
        var animals = _animalService.GetAnimals(orderBy);

        return Ok(animals);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal(int id)
    {
        var animal = _animalService.GetAnimal(id);
        if (animal == null)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }

        return Ok(animal);
    }
    
    [HttpPost]
    public IActionResult AddAnimal(Animal animal)
    {
        _animalService.AddAnimal(animal);
        return StatusCode(StatusCodes.Status201Created);
    }
    
    
    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(Animal animal, int id)
    {
        Animal animalTmp = _animalService.GetAnimal(id);
        if (animalTmp == null)
        {
            return BadRequest();
        }

        animal.IdAnimal = id;
        _animalService.UpdateAnimal(animal);

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int animalId)
    {
        int status = _animalService.DeleteAnimal(animalId);
        if (status == 1)
        {
            return NoContent();
        }
        return StatusCode(StatusCodes.Status404NotFound);
    }
}