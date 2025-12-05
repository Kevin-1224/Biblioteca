using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Modelos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    namespace ProyectoBiblioteca.Modelos
    {
        public class Prestamo
        {
            // Clave Primaria (PK)
            [Key] public int Id { get; set; }

            // Claves Foráneas (FKs)
            public int IdLibro { get; set; }
            public int IdUsuario { get; set; }
            public int IdBibliotecario { get; set; }

            // Atributos de la entidad
            public DateTime FechaPrestamo { get; set; }
            public DateTime FechaDevolucionEsperada { get; set; }

            // La fecha de devolución puede ser nula hasta que el libro se devuelva.
            public DateTime? FechaDevolucionReal { get; set; }

            // Propiedades de navegación (opcional)
            public Libro? Libro { get; set; } 
            public Usuario? Usuario { get; set; }
           public BibliotecaFisica? bibliotecafisica { get; set; }
        }
    }
}
