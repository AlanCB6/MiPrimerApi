import { useEffect, useState } from "react";
import "./App.css";

const API_URL = "https://localhost:7010";

export default function App() {
  const [seccion, setSeccion] = useState("clientes");
  const [datos, setDatos] = useState([]);
  const [cargando, setCargando] = useState(false);
  const [error, setError] = useState("");
  const [formData, setFormData] = useState({});

  const config = {
    clientes: {
      listar: `${API_URL}/Cliente/ListarClientes`,
      crear: `${API_URL}/Cliente/CrearCliente`,
      pk: "idClientes",
      campos: [
        { name: "nombre", label: "Nombre", type: "text" },
        { name: "apellidos", label: "Apellidos", type: "text" },
        { name: "telefono", label: "Teléfono", type: "text" },
      ],
    },
    empleados: {
      listar: `${API_URL}/Empleado/ListarEmpleados`,
      crear: `${API_URL}/Empleado/CrearEmpleado`,
      pk: "idEmpleado",
      campos: [
        { name: "nombre", label: "Nombre", type: "text" },
        { name: "apellido", label: "Apellido", type: "text" },
        { name: "idPuesto", label: "ID Puesto", type: "number" },
      ],
    },
    puestos: {
      listar: `${API_URL}/Puesto/ListarPuestos`,
      crear: `${API_URL}/Puesto/CrearPuesto`,
      pk: "idPuesto",
      campos: [{ name: "nombre", label: "Nombre Puesto", type: "text" }],
    },
  };

  const actualConfig = config[seccion];

  useEffect(() => {
    setFormData({});
    cargarDatos();
  }, [seccion]);

  async function cargarDatos() {
    setCargando(true);
    setError("");
    try {
      const res = await fetch(actualConfig.listar);
      if (!res.ok) throw new Error(`HTTP: ${res.status}`);
      const data = await res.json();
      setDatos(data);
    } catch (e) {
      setError(`No se pudieron cargar los datos de ${seccion}.`);
      console.error(e);
    } finally {
      setCargando(false);
    }
  }

  const handleChange = (e) => {
    const { name, value, type } = e.target;
    setFormData({
      ...formData,
      [name]: type === "number" ? Number(value) : value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const res = await fetch(actualConfig.crear, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData),
      });

      if (res.ok) {
        setFormData({});
        cargarDatos();
      } else {
        alert("Error al guardar en la base de datos.");
      }
    } catch (e) {
      console.error("Error al guardar:", e);
    }
  };

  return (
    <main className="contenedor">
      <header className="encabezado">
        <div>
          <h1>BD Empresa</h1>
          <p>Ingresar datos en BD</p>
        </div>

        <nav className="pestañas">
          <button
            className={seccion === "clientes" ? "activo" : ""}
            onClick={() => setSeccion("clientes")}
          >
            Clientes
          </button>
          <button
            className={seccion === "empleados" ? "activo" : ""}
            onClick={() => setSeccion("empleados")}
          >
            Empleados
          </button>
          <button
            className={seccion === "puestos" ? "activo" : ""}
            onClick={() => setSeccion("puestos")}
          >
            Puestos
          </button>
        </nav>
      </header>

      <section className="formulario-contenedor">
        <h3>Agregar nuevo {seccion.slice(0, -1)}</h3>
        <form onSubmit={handleSubmit} className="formulario">
          {actualConfig.campos.map((c) => (
            <div key={c.name} className="campo">
              <label>{c.label}:</label>
              <input
                type={c.type}
                name={c.name}
                value={formData[c.name] ?? ""}
                onChange={handleChange}
                required
              />
            </div>
          ))}
          <button type="submit" className="btn-guardar">
            Guardar
          </button>
        </form>
      </section>

      {cargando && <p>Cargando datos...</p>}

      {error && (
        <div className="error">
          <p>{error}</p>
          <button onClick={cargarDatos}>Reintentar</button>
        </div>
      )}

      {!cargando && !error && (
        <section className="tabla-contenedor">
          <p className="total">
            Total de registros ({seccion}): {datos.length}
          </p>

          <table>
            <thead>
              <tr>
                <th>ID</th>
                {actualConfig.campos.map((c) => (
                  <th key={c.name}>{c.label}</th>
                ))}
              </tr>
            </thead>

            <tbody>
              {datos.map((item) => {
                const id =
                  item[actualConfig.pk] ??
                  item[actualConfig.pk.toLowerCase()];
                return (
                  <tr key={id}>
                    <td>{id}</td>
                    {actualConfig.campos.map((c) => (
                      <td key={c.name}>
                        {item[c.name] ??
                          item[
                            c.name.charAt(0).toUpperCase() + c.name.slice(1)
                          ]}
                      </td>
                    ))}
                  </tr>
                );
              })}

              {datos.length === 0 && (
                <tr>
                  <td colSpan={actualConfig.campos.length + 1}>
                    No hay registros guardados.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </section>
      )}
    </main>
  );
}