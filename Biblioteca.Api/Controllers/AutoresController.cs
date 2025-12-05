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
    public class AutoresController : ControllerBase
    {
        private readonly BibliotecaApiContext _context;

        public AutoresController(BibliotecaApiContext context)
        {
            _context = context;
        }

        // GET: api/Autores
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Autor>>>> GetAutor()
        {
            try {                
                var data = await _context.Autor.ToListAsync();
                return ApiResult<List<Autor>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ApiResult<List<Autor>>.Fail(ex.Message);
            }
        }

        // GET: api/Autores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Autor>>> GetAutor(int id)
        {
            try {               
                var autor = await _context.Autor.FindAsync(id);
                if (autor == null)
                {
                    return ApiResult<Autor>.Fail("Autor no encontrado");
                }
                return ApiResult<Autor>.Ok(autor);
            }
            catch (Exception ex)
            {
                return ApiResult<Autor>.Fail(ex.Message);
            }
        }

        // PUT: api/Autores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Autor>>> PutAutor(int id, Autor autor)
        {
            if (id != autor.Id)
            {
                return ApiResult<Autor>.Fail("ID no coincide");
            }

            _context.Entry(autor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!AutorExists(id))
                {
                    return ApiResult<Autor>.Fail("Autor no encontrado");
                }
                else
                {
                    return ApiResult<Autor>.Fail(ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/Autores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Autor>>> PostAutor(Autor autor)
        {
            try {               
                _context.Autor.Add(autor);
                await _context.SaveChangesAsync();
                return ApiResult<Autor>.Ok(autor);
            }
            catch (Exception ex)
            {
                return ApiResult<Autor>.Fail(ex.Message);
            }
        }

        // DELETE: api/Autores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Autor>>> DeleteAutor(int id)
        {
            try
            {

                var autor = await _context.Autor.FindAsync(id);
                if (autor == null)
                {
                    return ApiResult<Autor>.Fail("Autor no encontrado");
                }
                _context.Autor.Remove(autor);
                await _context.SaveChangesAsync();
                return ApiResult<Autor>.Ok(null);
            }
            catch (Exception ex)
            {
                return ApiResult<Autor>.Fail(ex.Message);
            }
        }

        private bool AutorExists(int id)
        {
            return _context.Autor.Any(e => e.Id == id);
        }
    }
}
