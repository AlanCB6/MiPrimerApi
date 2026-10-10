using Microsoft.AspNetCore.Mvc;
using MiPrimerApi.Models;

namespace MiPrimerApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PuestoController : Controller
    {
        private readonly DbEmpresaContext _context;

        public PuestoController(DbEmpresaContext ctx)
        {
            _context = ctx;
        }

        [HttpGet("ListarPuestos")]
        public ActionResult<List<Puesto>> ListarPuestos()
        {
            return _context.Puestos.ToList();
        }

        [HttpGet("ObtenerPuesto/{id}")]
        public ActionResult<Puesto> ObtenerPuesto(int id)
        {
            var puesto = _context.Puestos.FirstOrDefault(e => e.IdPuesto == id);

            if (puesto == null)
            {
                return NotFound("Puesto no encontrado");
            }

            return puesto;
        }

        [HttpPost("CrearPuesto")]
        public ActionResult CrearPuesto([FromBody] Puesto puesto)
        {
            _context.Puestos.Add(puesto);
            _context.SaveChanges();

            return Ok("Puesto creado correctamente");
        }

        [HttpPut("ActualizarPuesto/{id}")]
        public ActionResult ActualizarPuesto(int id, [FromBody] Puesto puestoDatos)
        {
            var puesto = _context.Puestos.FirstOrDefault(e => e.IdPuesto == id);

            if (puesto == null)
            {
                return NotFound("Puesto no encontrado");
            }

            puesto.Nombre = puestoDatos.Nombre;

            _context.SaveChanges();

            return Ok("Puesto actualizado correctamente");
        }

        [HttpDelete("EliminarPuesto/{id}")]
        public ActionResult EliminarPuesto(int id)
        {
            var puesto = _context.Puestos.FirstOrDefault(e => e.IdPuesto == id);

            if (puesto == null)
            {
                return NotFound("Puesto no encontrado");
            }

            _context.Puestos.Remove(puesto);
            _context.SaveChanges();

            return Ok("Puesto eliminado correctamente");
        }
    }
}