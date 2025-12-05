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
    public class UsuariosController : ControllerBase
    {
        private readonly BibliotecaApiContext _context;

        public UsuariosController(BibliotecaApiContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Usuario>>>> GetUsuario()
        {
            try
            {
                var data = await _context.Usuario.ToListAsync();
                return ApiResult<List<Usuario>>.Ok(data);
            }
            catch (Exception ex) {
                return ApiResult<List<Usuario>>.Fail(ex.Message);
            }
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Usuario>>> GetUsuario(int id)
        {
            try
            {
                var usuario = await _context.Usuario.FindAsync(id);
                if (usuario == null)
                {
                    return ApiResult<Usuario>.Fail("Usuario no encontrado");
                }
                return ApiResult<Usuario>.Ok(usuario);
            }
            catch (Exception ex)
            {
                return ApiResult<Usuario>.Fail(ex.Message);
            }
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Usuario>>> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return ApiResult<Usuario>.Fail("ID de usuario no coincide");
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UsuarioExists(id))
                {
                    return ApiResult<Usuario>.Fail("Usuario no encontrado");
                }
                else
                {
                    return ApiResult<Usuario>.Fail(ex.Message); 
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Usuario>>> PostUsuario(Usuario usuario)
        {
            try { 
                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();
                return ApiResult<Usuario>.Ok(usuario);
            }
            catch (Exception ex)
            {
                return ApiResult<Usuario>.Fail(ex.Message);
            }
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Usuario>>> DeleteUsuario(int id)
        {
            try
            {
                var usuario = await _context.Usuario.FindAsync(id);
                if (usuario == null)
                {
                    return ApiResult<Usuario>.Fail("Usuario no encontrado");
                }
                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();
                return ApiResult<Usuario>.Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
