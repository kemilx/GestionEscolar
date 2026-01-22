using System;

namespace GestionEscolar.Models
{
    public class Evaluacion
    {
        public Estudiante Estudiante { get; }
        public Curso Curso { get; }

        private double _nota; 
        public double Nota
        {
            get => _nota;
            private set
            {
                if (value < 0 || value > 100) throw new ArgumentException("La nota debe estar entre 0 y 100.");
                _nota = value;
            }
        }

        public string Descripcion { get; }
        public DateTime Fecha { get; } = DateTime.Now;

        public Evaluacion(Estudiante estudiante, Curso curso, double nota, string descripcion)
        {
            Estudiante = estudiante ?? throw new ArgumentNullException(nameof(estudiante));
            Curso = curso ?? throw new ArgumentNullException(nameof(curso));
            Descripcion = (descripcion ?? "").Trim();
            if (Descripcion.Length < 3) throw new ArgumentException("Descripción inválida.");

            Nota = nota;
        }

        public override string ToString() => $"{Curso.Codigo} - {Estudiante.Nombre} - Nota: {Nota} - {Descripcion}";
    }
}
