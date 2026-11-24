using DSW_3P_PR03.Data;
using DSW_3P_PR03.Models;

namespace DSW_3P_PR03.Repositories;

public class PersonajeSQLRepository : IPersonajeRepository
{
    private readonly AppDbContext _context;

    public PersonajeSQLRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Personaje> ObtenerTodas()
    {
        return _context.Personajes.ToList();
    }

    public Personaje? ObtenerPorId(int id)
    {
        return _context.Personajes.Find(id);
    }

    public Personaje Agregar(Personaje mision)
    {
        mision.Id = 0;

        _context.Personajes.Add(mision);
        _context.SaveChanges();

        return mision;
    }

    public bool Actualizar(int id, Personaje mision)
    {
        var actual = _context.Personajes.Find(id);

        if(actual == null)
            return false;
        
        actual.Nombre = mision.Nombre;
        actual.Videojuego = mision.Videojuego;
        actual.Clase = mision.Clase;
        actual.Nivel = mision.Nivel;
        actual.Poder = mision.Poder;
        actual.Disponible = mision.Disponible;
        actual.ImagenUrl = mision.ImagenUrl;

        _context.SaveChanges();

        return true;
    }

    public bool Eliminar(int id)
    {
        var mision = _context.Personajes.Find(id);

        if (mision == null)
            return false;
        
        _context.Personajes.Remove(mision);
        _context.SaveChanges();

        return true;
    }
}