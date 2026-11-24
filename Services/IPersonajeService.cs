using DSW_3P_PR03.Models;

namespace DSW_3P_PR03.Services;

public interface IPersonajeService
{
    List<Personaje> ObtenerTodas();
    Personaje? ObtenerPorId(int id);
    List<Personaje> ConsultarPorJuego(string videojuego);
    List<Personaje> ConsultarPorNivel(int nivel);
    Personaje Agregar(Personaje nuevoPersonaje);
    bool Actualizar(int id, Personaje actualizado);
    bool Eliminar(int id);
}