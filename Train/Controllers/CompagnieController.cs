using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Train.Models.Entity;
using Train.Models.Repository.Managers;
using Train.Shared.DTO;

namespace Train.Controllers;

/// <summary>
/// Contrôleur REST permettant de gérer les compagnies ferroviaires.
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class CompagnieController : ControllerBase
{
    private readonly CompagnieManager _manager;
    private readonly IMapper _mapper;

    public CompagnieController(CompagnieManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    /// <summary>
    /// Récupère une compagnie à partir de son identifiant.
    /// </summary>
    /// <param name="id">Identifiant unique de la compagnie.</param>
    /// <returns>
    /// <list type="bullet">
    /// <item><description><see cref="CompagnieDetailDTO"/> si la compagnie existe (200).</description></item>
    /// <item><description><see cref="NotFoundResult"/> si aucune compagnie ne correspond (404).</description></item>
    /// </list>
    /// </returns>
    [ActionName("GetById")]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CompagnieDetailDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompagnieDetailDTO>> GetById(int id)
    {
        var result = await _manager.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return _mapper.Map<CompagnieDetailDTO>(result);
    }

    /// <summary>
    /// Récupère la liste de toutes les compagnies.
    /// </summary>
    /// <returns>Une collection de <see cref="CompagnieDTO"/> (200).</returns>
    [ActionName("GetAll")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CompagnieDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CompagnieDTO>>> GetAll()
    {
        var list = await _manager.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<CompagnieDTO>>(list));
    }

    /// <summary>
    /// Récupère une compagnie par son nom.
    /// </summary>
    /// <param name="nom">Nom de la compagnie.</param>
    /// <returns>La compagnie correspondante.</returns>
    [ActionName("GetByNom")]
    [HttpGet("{nom}")]
    [ProducesResponseType(typeof(CompagnieDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompagnieDTO>> GetByNom(string nom)
    {
        var result = await _manager.GetByNameAsync(nom);

        if (result is null)
            return NotFound();

        return _mapper.Map<CompagnieDTO>(result);
    }

    /// <summary>
    /// Récupère une compagnie par son email.
    /// </summary>
    /// <param name="email">Email de la compagnie.</param>
    /// <returns>La compagnie correspondante.</returns>
    [ActionName("GetByEmail")]
    [HttpGet("{email}")]
    [ProducesResponseType(typeof(CompagnieDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompagnieDTO>> GetByEmail(string email)
    {
        var result = await _manager.GetByEmailAsync(email);

        if (result is null)
            return NotFound();

        return _mapper.Map<CompagnieDTO>(result);
    }

    /// <summary>
    /// Crée une nouvelle compagnie.
    /// </summary>
    /// <param name="dto">Données de la compagnie à créer.</param>
    /// <returns>La compagnie créée.</returns>
    [ActionName("Post")]
    [HttpPost]
    [ProducesResponseType(typeof(CompagnieDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CompagnieDTO>> Post([FromBody] CompagnieCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = _mapper.Map<Compagnie>(dto);
        await _manager.AddAsync(entity);

        return CreatedAtAction(nameof(GetById), 
            new { id = entity.IdCompagnie }, 
            _mapper.Map<CompagnieDTO>(entity));
    }

    /// <summary>
    /// Met à jour une compagnie existante.
    /// </summary>
    /// <param name="id">Identifiant de la compagnie.</param>
    /// <param name="dto">Nouvelles données de la compagnie.</param>
    /// <returns>NoContent si succès.</returns>
    [ActionName("Put")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(int id, [FromBody] CompagnieUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var toUpdate = await _manager.GetByIdAsync(id);
        if (toUpdate == null)
            return NotFound();

        var updatedEntity = _mapper.Map<Compagnie>(dto);
        await _manager.UpdateAsync(toUpdate, updatedEntity);

        return NoContent();
    }

    /// <summary>
    /// Supprime une compagnie.
    /// </summary>
    /// <param name="id">Identifiant de la compagnie.</param>
    /// <returns>NoContent si succès.</returns>
    [ActionName("Delete")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _manager.GetByIdAsync(id);
        if (entity == null)
            return NotFound();

        await _manager.DeleteAsync(entity);
        return NoContent();
    }
}