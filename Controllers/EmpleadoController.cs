using Microsoft.AspNetCore.Mvc;
using MiPrimerApi.Models;

namespace MiPrimerApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EmpleadoController : Controller
    {
        private readonly DbEmpresaContext _context;

        public EmpleadoController(DbEmpresaContext ctx)
        {
            _context = ctx;
        }

        [HttpGet("ListarEmpleados")]
        public ActionResult<List<Empleado>> ListarEmpleados()
        {
            return _context.Empleados.ToList();
        }

        [HttpGet("ObtenerEmpleado/{id}")]
        public ActionResult<Empleado> ObtenerEmpleado(int id)
        {
            var empleado = _context.Empleados.FirstOrDefault(e => e.IdEmpleado == id);

            if (empleado == null)
            {
                return NotFound("Empleado no encontrado");
            }

            return empleado;
        }

        [HttpPost("CrearEmpleado")]
        public ActionResult CrearEmpleado([FromBody] Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            _context.SaveChanges();

            return Ok("Empleado creado correctamente");
        }

        [HttpPut("ActualizarEmpleado/{id}")]
        public ActionResult ActualizarEmpleado(int id, [FromBody] Empleado empleadoDatos)
        {
            var empleado = _context.Empleados.FirstOrDefault(e => e.IdEmpleado == id);

            if (empleado == null)
            {
                return NotFound("Empleado no encontrado");
            }

            empleado.IdPuesto = empleadoDatos.IdPuesto;
            empleado.Nombre = empleadoDatos.Nombre;
            empleado.Apellido = empleadoDatos.Apellido;

            _context.SaveChanges();

            return Ok("Empleado actualizado correctamente");
        }

        [HttpDelete("EliminarEmpleado/{id}")]
        public ActionResult EliminarEmpleado(int id)
        {
            var empleado = _context.Empleados.FirstOrDefault(e => e.IdEmpleado == id);

            if (empleado == null)
            {
                return NotFound("Empleado no encontrado");
            }

            _context.Empleados.Remove(empleado);
            _context.SaveChanges();

            return Ok("Empleado eliminado correctamente");
        }
    }
}