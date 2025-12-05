using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Modelos
{
    public class Autor
    {
        // Clave Primaria (PK)
        [Key]public int Id{ get; set; }

        // Atributos de la entidad
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Nacionalidad { get; set; } = string.Empty;

        // Propiedad de navegación (opcional, útil para ORMs como Entity Framework)
        public List<Libro> Libros { get; set; } = new List<Libro>();
       
    }
}
