using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Train.Models.Repository.Managers;
using Train.Shared.DTO;

namespace Train.Controllers;

/// <summary>
/// Contrôleur REST permettant de gérer les trains.
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class TrainController : ControllerBase
{
    private readonly TrainManager _manager;
    private readonly IMapper _mapper;

    public TrainController(TrainManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    /// <summary>
    /// Récupère un train à partir de son identifiant.
    /// </summary>
    /// <param name="id">Identifiant unique du train.</param>
    /// <returns>
    /// <list type="bullet">
    /// <item><description><see cref="TrainDetailDTO"/> si le train existe (200).</description></item>
    /// <item><description><see cref="NotFoundResult"/> si aucun train ne correspond (404).</description></item>
    /// </list>
    /// </returns>
    [ActionName("GetById")]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TrainDetailDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrainDetailDTO>> GetById(int id)
    {
        var result = await _manager.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return _mapper.Map<TrainDetailDTO>(result);
    }

    /// <summary>
    /// Récupère la liste de tous les trains.
    /// </summary>
    /// <returns>Une collection de <see cref="TrainDTO"/> (200).</returns>
    [ActionName("GetAll")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TrainDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TrainDTO>>> GetAll()
    {
        var list = await _manager.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<TrainDTO>>(list));
    }

    /// <summary>
    /// Récupère les trains d'une compagnie.
    /// </summary>
    /// <param name="idCompagnie">Identifiant de la compagnie.</param>
    /// <returns>Une collection de trains.</returns>
    [ActionName("GetByCompagnie")]
    [HttpGet("{idCompagnie}")]
    [ProducesResponseType(typeof(IEnumerable<TrainDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<TrainDTO>>> GetByCompagnie(int idCompagnie)
    {
        var trains = await _manager.GetTrainsByCompagnieAsync(idCompagnie);
        
        if (!trains.Any())
            return NotFound();

        return Ok(_mapper.Map<IEnumerable<TrainDTO>>(trains));
    }

    /// <summary>
    /// Récupère tous les trains actifs.
    /// </summary>
    /// <returns>Une collection de trains actifs.</returns>
    [ActionName("GetActifs")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TrainDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TrainDTO>>> GetActifs()
    {
        var trains = await _manager.GetTrainsActifsAsync();
        return Ok(_mapper.Map<IEnumerable<TrainDTO>>(trains));
    }

    /// <summary>
    /// Crée un nouveau train.
    /// </summary>
    /// <param name="dto">Données du train à créer.</param>
    /// <returns>Le train créé.</returns>
    [ActionName("Post")]
    [HttpPost]
    [ProducesResponseType(typeof(TrainDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TrainDTO>> Post([FromBody] TrainCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = _mapper.Map<Train.Models.Entity.Train>(dto);
        await _manager.AddAsync(entity);

        return CreatedAtAction(nameof(GetById), 
            new { id = entity.IdTrain }, 
            _mapper.Map<TrainDTO>(entity));
    }

    /// <summary>
    /// Met à jour un train existant.
    /// </summary>
    /// <param name="id">Identifiant du train.</param>
    /// <param name="dto">Nouvelles données du train.</param>
    /// <returns>NoContent si succès.</returns>
    [ActionName("Put")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(int id, [FromBody] TrainUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var toUpdate = await _manager.GetByIdAsync(id);
        if (toUpdate == null)
            return NotFound();

        var updatedEntity = _mapper.Map<Train.Models.Entity.Train>(dto);
        await _manager.UpdateAsync(toUpdate, updatedEntity);

        return NoContent();
    }

    /// <summary>
    /// Supprime un train.
    /// </summary>
    /// <param name="id">Identifiant du train.</param>
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