using Microsoft.AspNetCore.Mvc;
using RR.Livraria.Domain.Interfaces.Services;
using RR.Livraria.Domain.ViewModels;

namespace RR.Livraria.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LivroController : ControllerBase
{
    private readonly ILivroService _services;
    private readonly ILogger<LivroController> _logger;

    public LivroController(ILivroService services, ILogger<LivroController> logger)
    {
        _services = services;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LivroViewModel>>> GetAll()
        => Ok(await _services.GetAllAsync());

    [HttpGet("{code}")]
    public async Task<ActionResult<LivroViewModel>> GetByCode(int code)
    {
        var result = await _services.GetByCodeAsync(code);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<LivroViewModel>> Post([FromBody] LivroNewViewModel model)
        => Ok(await _services.AddAsync(model));

    [HttpPut("{code}")]
    public async Task<ActionResult<LivroViewModel>> Put(int code, [FromBody] LivroViewModel model)
    {
        var entity = await _services.GetByCodeAsync(code);
        if (entity == null)
            return NotFound();
        model.Codl = code;
        return Ok(await _services.UpdateAsync(model));
    }

    [HttpDelete("{code}")]
    public async Task<ActionResult<bool>> Delete(int code)
        => Ok(await _services.RemoveAsync(code));
}
