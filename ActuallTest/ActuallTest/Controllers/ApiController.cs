using Microsoft.AspNetCore.Mvc;
using TestPrep2.Context;

namespace TestPrep2.Controllers;

public class ApiController: ControllerBase
{
    private readonly LocalDbContext _context;

    public ApiController(LocalDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservations(int id)
    {
        return Ok();
    }

    [HttpPost("makeReservation")]
    public async Task<IActionResult> MakeReservation()
    {
        return Ok();
    }
}