using Microsoft.AspNetCore.Mvc;
using RR.Livraria.Domain.Interfaces.Services;
using RR.Livraria.Domain.ViewModels;

namespace RR.Livraria.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VendaController : ControllerBase
{
    private readonly IVendaService _services;
    private readonly ILogger<VendaController> _logger;

    public VendaController(IVendaService services, ILogger<VendaController> logger)
    {
        _services = services;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VendaViewModel>>> GetAll()
        => Ok(await _services.GetAllAsync());

    [HttpGet("{code}")]
    public async Task<ActionResult<VendaViewModel>> GetByCode(int code)
    {
        var result = await _services.GetByCodeAsync(code);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<VendaViewModel>> Post([FromBody] VendaNewViewModel model)
        => Ok(await _services.AddAsync(model));

    [HttpPut("{code}")]
    public async Task<ActionResult<VendaViewModel>> Put(int code, [FromBody] VendaViewModel model)
    {
        var entity = await _services.GetByCodeAsync(code);
        if (entity == null)
            return NotFound();
        model.CodV = code;
        return Ok(await _services.UpdateAsync(model));
    }

    [HttpDelete("{code}")]
    public async Task<ActionResult<bool>> Delete(int code)
        => Ok(await _services.DeleteAsync(code));
}
