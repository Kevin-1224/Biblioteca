using Biblioteca.Modelos.ProyectoBiblioteca.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Modelos
{
    public class BibliotecaFisica
    {
        [Key] public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public List<Libro> Libros { get; set; } = new List<Libro>();
        public List<Prestamo> prestamos { get; set; } = new List<Prestamo>();
    }
}
