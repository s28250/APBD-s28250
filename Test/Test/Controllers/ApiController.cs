using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Context;
using Test.DTO;
using Test.Models;

namespace Test.Controllers;
[ApiController]
[Route("/api[controller]")]
public class ApiController : ControllerBase
{
    private readonly LocalDbContext _context;

    public ApiController(LocalDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        return await _context.Clients.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClient(int id)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteClient(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> CreateClient([FromBody] ClientDTO request)
    {
        var client = new Client()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Telephone = request.Telephone,
            Pesel = request.Pesel
        };
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return Ok( "Created client id: " + client.IdClient);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateClient([FromBody] ClientDTO request, int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }

        client.FirstName = request.FirstName;
        client.LastName = request.LastName;
        _context.Entry(client).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}