using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Models;
using PortalProductos.Models;

namespace Portalclientes1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly NubeDB _context;

        public ProductosController(NubeDB context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        [HttpGet("{cliente}/{idCliente}")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos(int idCliente)
        {
            return await _context.Productos.Where(p => p.IdCliente == idCliente).ToListAsync();
        }


        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }

            return NoContent();
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(CrearProducto model)
        {
                var random = new Random();
            var producto = new Producto()
            {
                Codigo = random.Next(0, 999).ToString(),
                Tipo = model.TipoProducto,
                IdCliente = model.ClienteId
            };
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return Redirect(model.UrlRetorno);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducto(int id, int idCliente, string urlRetorno)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return Redirect(urlRetorno);
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
