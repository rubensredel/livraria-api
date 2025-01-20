using Microsoft.AspNetCore.Mvc;
using RR.Livraria.Domain.Interfaces.Services;
using RR.Livraria.Domain.ViewModels;

namespace RR.Livraria.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AutorController : ControllerBase
{
    private readonly IAutorService _services;
    private readonly ILogger<AutorController> _logger;
    
    public AutorController(IAutorService services, ILogger<AutorController> logger)
    {
        _services = services;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AutorViewModel>>> GetAll()
        => Ok(await _services.GetAllAsync());

    [HttpGet("{code}")]
    public async Task<ActionResult<AutorViewModel>> GetByCode(int code)
    {
        var result = await _services.GetByCodeAsync(code);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<AutorViewModel>> Post([FromBody] AutorNewViewModel model)
        => Ok(await _services.AddAsync(model));

    [HttpPut("{code}")]
    public async Task<ActionResult<AutorViewModel>> Put(int code, [FromBody] AutorViewModel model)
    {
        var entity = await _services.GetByCodeAsync(code);
        if (entity == null)
            return NotFound();
        model.CodAu = code;
        return Ok(await _services.UpdateAsync(model));
    }

    [HttpDelete("{code}")]
    public async Task<ActionResult<bool>> Delete(int code)
        => Ok(await _services.RemoveAsync(code));
}
