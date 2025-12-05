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
    public class BibliotecaFisicasController : ControllerBase
    {
        private readonly BibliotecaApiContext _context;

        public BibliotecaFisicasController(BibliotecaApiContext context)
        {
            _context = context;
        }

        // GET: api/BibliotecaFisicas
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<BibliotecaFisica>>>> GetBiblioteca()
        {
            try {                 
                var data = await _context.Biblioteca.ToListAsync();
                return ApiResult<List<BibliotecaFisica>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<BibliotecaFisica>>.Fail(ex.Message);
            }

        }

        // GET: api/BibliotecaFisicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<BibliotecaFisica>>> GetBibliotecaFisica(int id)
        {
            try {                
                var bibliotecaFisica = await _context.Biblioteca.FindAsync(id);
                if (bibliotecaFisica == null)
                {
                    return ApiResult<BibliotecaFisica>.Fail("Biblioteca no encontrada");
                }
                return ApiResult<BibliotecaFisica>.Ok(bibliotecaFisica);
            }
            catch (Exception ex)
            {
                return ApiResult<BibliotecaFisica>.Fail(ex.Message);
            }
            
        }

        // PUT: api/BibliotecaFisicas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<BibliotecaFisica>>> PutBibliotecaFisica(int id, BibliotecaFisica bibliotecaFisica)
        {
            
            if (id != bibliotecaFisica.Id)
            {
                return ApiResult<BibliotecaFisica>.Fail("ID de la biblioteca no coincide");
            }

            _context.Entry(bibliotecaFisica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BibliotecaFisicaExists(id))
                {
                    return ApiResult<BibliotecaFisica>.Fail("Biblioteca no encontrada");
                }
                else
                {
                    return ApiResult<BibliotecaFisica>.Fail(ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/BibliotecaFisicas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<BibliotecaFisica>>> PostBibliotecaFisica(BibliotecaFisica bibliotecaFisica)
        {
            try {               
                _context.Biblioteca.Add(bibliotecaFisica);
                await _context.SaveChangesAsync();
                return ApiResult<BibliotecaFisica>.Ok(bibliotecaFisica);
            }
            catch (Exception ex)
            {
                return ApiResult<BibliotecaFisica>.Fail(ex.Message);
            }
            
        }

        // DELETE: api/BibliotecaFisicas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<BibliotecaFisica>>> DeleteBibliotecaFisica(int id)
        {
            try {               
                var bibliotecaFisica = await _context.Biblioteca.FindAsync(id);
                if (bibliotecaFisica == null)
                {
                    return ApiResult<BibliotecaFisica>.Fail("Biblioteca no encontrada");
                }
                _context.Biblioteca.Remove(bibliotecaFisica);
                await _context.SaveChangesAsync();
                return ApiResult<BibliotecaFisica>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<BibliotecaFisica>.Fail(ex.Message);
            }
           
        }

        private bool BibliotecaFisicaExists(int id)
        {
            return _context.Biblioteca.Any(e => e.Id == id);
        }
    }
}
