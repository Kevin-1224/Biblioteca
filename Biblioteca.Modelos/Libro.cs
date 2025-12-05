using Biblioteca.Modelos.ProyectoBiblioteca.Modelos;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Modelos
{
    public class Libro
    {
        // Clave Primaria (PK)
        [Key]public int Id { get; set; }

        // Atributos de la entidad
        public string Titulo { get; set; } 
        public int AnioPublicacion { get; set; }

        // Clave Foránea (FK) para relacionarlo con Autor
        public int IdAutor { get; set; }
        public int IdBibliotecaFisica { get; set; }

        // Propiedades de navegación (opcional)
        public Autor? Autor { get; set; } 
        public BibliotecaFisica? BibliotecaFisica { get; set; }
        public List<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}
