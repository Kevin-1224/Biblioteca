using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Modelos.ProyectoBiblioteca.Modelos;
using Biblioteca.Modelos;

namespace Biblioteca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly BibliotecaApiContext _context;

        public PrestamosController(BibliotecaApiContext context)
        {
            _context = context;
        }

        // GET: api/Prestamos
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Prestamo>>>> GetPrestamo()
        {
            try
            {
                var data = await _context.Prestamo.ToListAsync();
                return ApiResult<List<Prestamo>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Prestamo>>.Fail(ex.Message);

            }
        }

        // GET: api/Prestamos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Prestamo>>> GetPrestamo(int id)
        {
            try
            {
                var prestamo = await _context.Prestamo.FindAsync(id);
                if (prestamo == null)
                {
                    return ApiResult<Prestamo>.Fail("Prestamo no encontrado");
                }
                return ApiResult<Prestamo>.Ok(prestamo);
            }
            catch (Exception ex)
            {
                return ApiResult<Prestamo>.Fail(ex.Message);
            }
        }

        // PUT: api/Prestamos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Prestamo>>> PutPrestamo(int id, Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return ApiResult<Prestamo>.Fail("ID no coincide");
            }

            _context.Entry(prestamo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PrestamoExists(id))
                {
                    return ApiResult<Prestamo>.Fail("Prestamo no encontrado");
                }
                else
                {
                    return ApiResult<Prestamo>.Fail(ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/Prestamos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Prestamo>>> PostPrestamo(Prestamo prestamo)
        {
            try { 
                _context.Prestamo.Add(prestamo);
                await  _context.SaveChangesAsync();
                return ApiResult<Prestamo>.Ok(prestamo);
            }
            catch (Exception ex)
            {
                return ApiResult<Prestamo>.Fail(ex.Message);
            }
            
        }

        // DELETE: api/Prestamos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Prestamo>>> DeletePrestamo(int id)
        {
            try { 
                var prestamo = await _context.Prestamo.FindAsync(id);
                if (prestamo == null)
                {
                    return ApiResult<Prestamo>.Fail("Prestamo no encontrado");
                }
                _context.Prestamo.Remove(prestamo);
                await _context.SaveChangesAsync();
                return ApiResult<Prestamo>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Prestamo>.Fail(ex.Message);
            }
            
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamo.Any(e => e.Id == id);
        }
    }
}
