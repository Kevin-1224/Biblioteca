using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Modelos;
using Biblioteca.Modelos.ProyectoBiblioteca.Modelos;

    public class BibliotecaApiContext : DbContext
    {
        public BibliotecaApiContext (DbContextOptions<BibliotecaApiContext> options)
            : base(options)
        {
        }

        public DbSet<Biblioteca.Modelos.Autor> Autor { get; set; } = default!;

public DbSet<Biblioteca.Modelos.BibliotecaFisica> Biblioteca { get; set; } = default!;

public DbSet<Biblioteca.Modelos.Libro> Libro { get; set; } = default!;

public DbSet<Biblioteca.Modelos.ProyectoBiblioteca.Modelos.Prestamo> Prestamo { get; set; } = default!;

public DbSet<Biblioteca.Modelos.Usuario> Usuario { get; set; } = default!;
    }
