using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Modelos;

namespace Biblioteca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly BibliotecaApiContext _context;

        public LibrosController(BibliotecaApiContext context)
        {
            _context = context;
        }

        // GET: api/Libros
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Libro>>>> GetLibro()
        {
            try
            {
                var data = await _context.Libro.ToListAsync();
                return ApiResult<List<Libro>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Libro>>.Fail(ex.Message); 
            }
        }

        // GET: api/Libros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Libro>>> GetLibro(int id)
        {
            try { 
                var libro = await _context.Libro.Include(l => l.Autor)
                                               .Include(l => l.BibliotecaFisica)
                                               .FirstOrDefaultAsync(l => l.Id == id);
                if (libro == null)
                {
                    return ApiResult<Libro>.Fail("Libro no encontrado");
                }
                return ApiResult<Libro>.Ok(libro);
            }
            catch (Exception ex)
            {
                return ApiResult<Libro>.Fail(ex.Message);
            }
        }

        // PUT: api/Libros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Libro>>> PutLibro(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return ApiResult<Libro>.Fail("ID del libro no coincide");
            }

            _context.Entry(libro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!LibroExists(id))
                {
                    return ApiResult<Libro>.Fail("Libro no encontrado");
                }
                else
                {
                    return ApiResult<Libro>.Fail(ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/Libros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Libro>>> PostLibro(Libro libro)
        {
            try { 
                _context.Libro.Add(libro);
                await _context.SaveChangesAsync();
                return ApiResult<Libro>.Ok(libro);
            }
            catch (Exception ex)
            {
                return ApiResult<Libro>.Fail(ex.Message);
            }
            
        }

        // DELETE: api/Libros/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Libro>>> DeleteLibro(int id)
        {
            try {
                var libro = await _context.Libro.FindAsync(id);
                if (libro == null)
                {
                    return ApiResult<Libro>.Fail("Libro no encontrado");
                }

                _context.Libro.Remove(libro);
                await _context.SaveChangesAsync();

                return ApiResult<Libro>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Libro>.Fail(ex.Message);
            }

        }

        private bool LibroExists(int id)
        {
            return _context.Libro.Any(e => e.Id == id);
        }
    }
}
