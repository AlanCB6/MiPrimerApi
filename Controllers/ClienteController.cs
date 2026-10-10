using Microsoft.AspNetCore.Mvc;
using MiPrimerApi.Models;

namespace MiPrimerApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ClienteController : Controller
    {
        private readonly DbEmpresaContext _context;

        public ClienteController(DbEmpresaContext ctx)
        {
            _context = ctx;
        }

        [HttpGet("ListarClientes")]
        public ActionResult<List<Cliente>> ListarClientes()
        {
            return _context.Clientes.ToList();
        }

        [HttpGet("ObtenerCliente/{id}")]
        public ActionResult<Cliente> ObtenerCliente(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(e => e.IdClientes == id);

            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            return cliente;
        }

        [HttpPost("CrearCliente")]
        public ActionResult CrearCliente([FromBody] Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return Ok("Cliente creado correctamente");
        }

        [HttpPut("ActualizarCliente/{id}")]
        public ActionResult ActualizarCliente(int id, [FromBody] Cliente clienteDatos)
        {
            var cliente = _context.Clientes.FirstOrDefault(e => e.IdClientes == id);

            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            cliente.Nombre = clienteDatos.Nombre;
            cliente.Apellidos = clienteDatos.Apellidos;
            cliente.Telefono = clienteDatos.Telefono;

            _context.SaveChanges();

            return Ok("Cliente actualizado correctamente");
        }

        [HttpDelete("EliminarCliente/{id}")]
        public ActionResult EliminarCliente(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(e => e.IdClientes == id);

            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return Ok("Cliente eliminado correctamente");
        }
    }
}