using Microsoft.AspNetCore.Mvc;
using RR.Livraria.Domain.Interfaces.Services;
using RR.Livraria.Domain.ViewModels;

namespace RR.Livraria.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AssuntoController : ControllerBase
{
    private readonly IAssuntoService _service;
    private readonly ILogger<AssuntoController> _logger;

    public AssuntoController(IAssuntoService service, ILogger<AssuntoController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssuntoNewViewModel>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{code}")]
    public async Task<ActionResult<AssuntoNewViewModel>> GetByCode(int code)
    {
        var result = await _service.GetByCodeAsync(code);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<AssuntoViewModel>> Post([FromBody] AssuntoNewViewModel model)
        => Ok(await _service.AddAsync(model));

    [HttpPut("{code}")]
    public async Task<ActionResult<AssuntoNewViewModel>> Put(int code, [FromBody] AssuntoViewModel model)
    {
        var entity = await _service.GetByCodeAsync(code);
        if (entity == null)
            return NotFound();
        model.CodAs = code;
        return Ok(await _service.UpdateAsync(model));
    }

    [HttpDelete("{code}")]
    public async Task<ActionResult<bool>> Delete(int code)
        => Ok(await _service.RemoveAsync(code));
}
