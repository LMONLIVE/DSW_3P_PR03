using DSW_3P_PR03.Models;
using System.Text.Json;

namespace DSW_3P_PR03.Repositories;

public class PersonajeJSONRepository : IPersonajeRepository
{

    private readonly string _ruta;
    private readonly JsonSerializerOptions _opciones = new()
    {
        WriteIndented = true
    };

    public PersonajeJSONRepository(IWebHostEnvironment environment)
    {
        _ruta = Path.Combine(environment.ContentRootPath, "Data", "personajes.json");

        Directory.CreateDirectory(Path.GetDirectoryName(_ruta)!);

        if (!File.Exists(_ruta))
        {
            File.WriteAllText(_ruta, "[]");
        }
    }

    private List<Personaje> Leer()
    {
        string json = File.ReadAllText(_ruta);

        return JsonSerializer.Deserialize<List<Personaje>>(json) ?? new List<Personaje>();
    }

    private void Guardar(List<Personaje> datos)
    {
        string json = JsonSerializer.Serialize(datos, _opciones);

        File.WriteAllText(_ruta, json);
    }

    public List<Personaje> ObtenerTodas()
    {
        return Leer();
    }

    public Personaje? ObtenerPorId(int id)
    {
        return Leer().FirstOrDefault(d => d.Id == id);
    }

    public Personaje Agregar(Personaje nueva)
    {
        var datos = Leer();

        nueva.Id = datos.Count == 0 ? 1 : datos.Max(d => d.Id) + 1;
        datos.Add(nueva);
        Guardar(datos);
        
        return nueva;
    }

    public bool Actualizar(int id, Personaje actualizada)
    {
        var datos = Leer();

        var existente = datos.FirstOrDefault(d => d.Id == id);

        if (existente == null)
            return false;
        
        existente.Nombre = actualizada.Nombre;
        existente.Videojuego = actualizada.Videojuego;
        existente.Clase = actualizada.Clase;
        existente.Nivel = actualizada.Nivel;
        existente.Poder = actualizada.Poder;
        existente.Disponible = actualizada.Disponible;
        existente.ImagenUrl = actualizada.ImagenUrl;

        Guardar(datos);
        return true;
    }

    public bool Eliminar(int id)
    {
        var datos = Leer();
        
        var existente = datos.FirstOrDefault(d => d.Id == id);

        if(existente == null)
            return false;
        
        datos.Remove(existente);
        Guardar(datos);
        return true;
    }
}