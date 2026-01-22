using System.Linq;
using System.Text;
using GestionEscolar.Models;

namespace GestionEscolar.Services.Reportes
{
    public class ReportePromedios : IReporte
    {
        private readonly Services.EscuelaService _escuela;

        public string Nombre => "Reporte de promedios por estudiante";

        public ReportePromedios(Services.EscuelaService escuela)
        {
            _escuela = escuela;
        }

        public string Generar()
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== PROMEDIOS ===");

            var estudiantes = _escuela.Usuarios.OfType<Estudiante>().ToList();
            if (estudiantes.Count == 0) return "No hay estudiantes.";

            foreach (var e in estudiantes)
            {
                var prom = _escuela.PromedioEstudiante(e.Id);
                sb.AppendLine($"{e.Nombre} ({e.Id}) => {prom}");
            }
            return sb.ToString();
        }
    }
}
