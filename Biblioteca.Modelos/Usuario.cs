using Biblioteca.Modelos.ProyectoBiblioteca.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Modelos
{
    public class Usuario
    {
        // Clave Primaria (PK)
        [Key] public int Id { get; set; }

        // Atributos de la entidad
        public string Nombre { get; set; } 
        public string Apellido { get; set; } 
        public string Email { get; set; } 
        public string Direccion { get; set; } 

        // Propiedad de navegación (opcional)
        public List<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    }
}
