using System;

namespace GestionEscolar.Models
{
    public class Inscripcion
    {
        public Estudiante Estudiante { get; }
        public Curso Curso { get; }

        public DateTime Fecha { get; } = DateTime.Now;

        public Inscripcion(Estudiante estudiante, Curso curso)
        {
            Estudiante = estudiante ?? throw new ArgumentNullException(nameof(estudiante));
            Curso = curso ?? throw new ArgumentNullException(nameof(curso));
        }

        public override string ToString() => $"{Estudiante.Nombre} inscrito en {Curso.Codigo} ({Fecha:yyyy-MM-dd})";
    }
}
