using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Models;
using Portalclientes1.Servicios;

namespace Portalclientes1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly NubeDB _context;
        private readonly IServicioEncriptacion _servicioEncriptacion;

        public ClientesController(NubeDB context, IServicioEncriptacion servicioEncriptacion)
        {
            _context = context;
            _servicioEncriptacion = servicioEncriptacion;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes =  await _context.Clientes.ToListAsync();
            foreach (var cliente in clientes)
            {
                cliente.DocumentoIdentidad = _servicioEncriptacion.DesencriptarDocumento(cliente.DocumentoIdentidad);
            }
            return clientes;
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.DocumentoIdentidad = _servicioEncriptacion.DesencriptarDocumento(cliente.DocumentoIdentidad);

            return cliente;
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            var documento = cliente.DocumentoIdentidad;
            cliente.DocumentoIdentidad = _servicioEncriptacion.EncriptacionDocumento(documento);

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            var documento = cliente.DocumentoIdentidad;
            cliente.DocumentoIdentidad = _servicioEncriptacion.EncriptacionDocumento(documento);

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
