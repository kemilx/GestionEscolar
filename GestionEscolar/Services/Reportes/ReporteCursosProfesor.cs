using System.Linq;
using System.Text;
using GestionEscolar.Models;

namespace GestionEscolar.Services.Reportes
{
    public class ReporteCursosProfesor : IReporte
    {
        private readonly Services.EscuelaService _escuela;

        public string Nombre => "Reporte de cursos por profesor";

        public ReporteCursosProfesor(Services.EscuelaService escuela)
        {
            _escuela = escuela;
        }

        public string Generar()
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== CURSOS POR PROFESOR ===");

            var profesores = _escuela.Usuarios.OfType<Profesor>().ToList();
            if (profesores.Count == 0) return "No hay profesores.";

            foreach (var p in profesores)
            {
                var cursos = _escuela.Cursos.Where(c => c.ProfesorAsignado.Id == p.Id).ToList();
                sb.AppendLine($"Profesor: {p.Nombre} ({p.Id})");
                if (cursos.Count == 0) sb.AppendLine("  - (Sin cursos)");
                foreach (var c in cursos)
                    sb.AppendLine($"  - {c.Codigo} {c.Nombre}");
            }

            return sb.ToString();
        }
    }
}
