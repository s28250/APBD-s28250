using Microsoft.AspNetCore.Mvc;
using SampleAPIControllers.Models;

namespace SampleAPIControllers.Controllers;


[Route("api/[controller]")]
[ApiController]
public class VisitsController : ControllerBase
{
    private static readonly List<Visit> _visits = new();

    [HttpGet("/api/animals/{animalId:int}/visits")]
    public IActionResult GetVisitsForAnimal(int animalId)
    {
        var animalVisits = _visits.Where(v => v.AnimalId == animalId).ToList();
        if (!animalVisits.Any()) return NotFound($"No visits found for animal ID {animalId}");
        return Ok(animalVisits);
    }

    [HttpPost]
    public IActionResult CreateVisit(Visit visit)
    {
        _visits.Add(visit);
        return Created($"/api/visits/{visit.Id}", visit);
    }
}