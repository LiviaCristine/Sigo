using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigoWebApp.Models;
using System.Net;

namespace SigoWebApp.Controllers
{
   [ApiController]
[Route("api/[controller]")]
public class EmpresaController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmpresaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empresa>>> GetAll()
    {
        return await _context.Empresas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Empresa>> GetById(int id)
    {
        var empresa = await _context.Empresas.FindAsync(id);
        return empresa == null ? NotFound() : empresa;
    }

    [HttpPost]
    public async Task<ActionResult<Empresa>> Create(Empresa empresa)
    {
        _context.Empresas.Add(empresa);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = empresa.Id }, empresa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Empresa empresa)
    {
        if (id != empresa.Id) return BadRequest();
        _context.Entry(empresa).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var empresa = await _context.Empresas.FindAsync(id);
        if (empresa == null) return NotFound();
        _context.Empresas.Remove(empresa);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

}
