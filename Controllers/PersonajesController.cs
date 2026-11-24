using Microsoft.AspNetCore.Mvc;
using DSW_3P_PR03.Services;
using DSW_3P_PR03.Models;

namespace DSW_3P_PR03.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonajesController : ControllerBase
{
    private readonly IPersonajeService _service;

    public PersonajesController(IPersonajeService service)
    {
        _service = service;
    }

    //GET - Obtener todos los personajes
    [HttpGet]
    [ProducesResponseType(typeof(List<Personaje>),200)]
    public IActionResult ObtenerTodas()
    {
        return Ok(_service.ObtenerTodas());
    }


    //GET - Obtener por id
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Personaje),200)]
    [ProducesResponseType(404)]
    public IActionResult ObtenerPorId(int id)
    {
        var personaje = _service.ObtenerPorId(id);

        if (personaje == null)
            return NotFound();
        
        return Ok(personaje);
    }

    [HttpGet("videojuego/{videojuego}")]
    [ProducesResponseType(typeof(List<Personaje>),200)]
    [ProducesResponseType(404)]
    public IActionResult ConsultarPorJuego(string videojuego)
    {
        var lista = _service.ConsultarPorJuego(videojuego);

        if (lista.Count == 0)
            return NotFound();
        
        return Ok(lista);
    }

    [HttpGet("nivel/{nivel:int}")]
    [ProducesResponseType(typeof(List<Personaje>),200)]
    [ProducesResponseType(404)]
    public IActionResult ConsultarPorNivel(int nivel)
    {
        var lista = _service.ConsultarPorNivel(nivel);

        if (lista.Count == 0)
            return NotFound();
        
        return Ok(lista);
    }


    //POST - Agregar personaje
    [HttpPost]
    [ProducesResponseType(typeof(Personaje),200)]
    [ProducesResponseType(400)]
    public IActionResult Agregar([FromBody] Personaje personaje)
    {
        try
        {
            var creada = _service.Agregar(personaje);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new {id= creada.Id},
                creada
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new {mensaje = ex.Message});
        }
    }

    //PUT - Actualizar Personaje
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public IActionResult Actualizar(int id, [FromBody] Personaje personaje)
    {
        try
        {
            if(!_service.Actualizar(id, personaje))
                return NotFound();

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new {mensaje = ex.Message});
        }
    }

    //DELETE - Eliminar personaje
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult Eliminar(int id)
    {
        if(!_service.Eliminar(id))
            return NotFound();
        
        return NoContent();
    }
}