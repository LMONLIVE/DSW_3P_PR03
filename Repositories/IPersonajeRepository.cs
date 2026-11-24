using DSW_3P_PR03.Models;

namespace DSW_3P_PR03.Repositories;

public interface IPersonajeRepository
{
    List<Personaje> ObtenerTodas();
    Personaje? ObtenerPorId(int id);
    Personaje Agregar(Personaje nuevoPersonaje);
    bool Actualizar(int id, Personaje Personaje);
    bool Eliminar(int id);
}