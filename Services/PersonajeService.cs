using DSW_3P_PR03.Models;
using DSW_3P_PR03.Repositories;

namespace DSW_3P_PR03.Services;

public class PersonajeService : IPersonajeService
{
    private readonly IPersonajeRepository _repository;

    public PersonajeService(IPersonajeRepository repository)
    {
        _repository = repository;
    }

    private static void Validar(Personaje personaje)
    {
        if (string.IsNullOrWhiteSpace(personaje.Nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(personaje.Videojuego))
            throw new ArgumentException("El videojuego es obligatorio");

        if (string.IsNullOrWhiteSpace(personaje.Clase))
            throw new ArgumentException("La clase es obligatorio");

        if(personaje.Poder < 1 || personaje.Poder > 100)
            throw new ArgumentException("El poder debe de estar entre 1 y 100");
    }

    public List<Personaje> ObtenerTodas()
    {
        return _repository.ObtenerTodas();
    }

    public Personaje? ObtenerPorId(int id)
    {
        var personaje = _repository.ObtenerPorId(id);

        return personaje == null ? null : personaje;
    }

    public List<Personaje> ConsultarPorJuego(string videojuego)
    {
        var lista = _repository.ObtenerTodas().AsEnumerable();

        lista = lista.Where(v => v.Videojuego.Equals(videojuego, StringComparison.OrdinalIgnoreCase));

        return lista.ToList();
    }

    public List<Personaje> ConsultarPorNivel(int nivel)
    {
        var lista = _repository.ObtenerTodas().AsEnumerable();

        lista = lista.Where(n => n.Nivel >= nivel);

        return lista.ToList();
    }

    public Personaje Agregar(Personaje personaje)
    {
        Validar(personaje);
        personaje.Disponible = true;

        return _repository.Agregar(personaje);
    }

    public bool Actualizar(int id, Personaje personaje)
    {
        Validar(personaje);

        return _repository.Actualizar(id, personaje);
    }

    public bool Eliminar(int id)
    {
        return _repository.Eliminar(id);
    }
}  