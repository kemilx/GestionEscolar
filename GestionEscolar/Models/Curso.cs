using System;

namespace GestionEscolar.Models
{
    public class Curso
    {
        public string Codigo { get; }

        private string _nombre;
        public string Nombre
        {
            get => _nombre;
            private set
            {
                value = (value ?? "").Trim();
                if (value.Length < 3) throw new ArgumentException("Nombre de curso inválido.");
                _nombre = value;
            }
        }

        public Profesor ProfesorAsignado { get; private set; }

        public Curso(string codigo, string nombre, Profesor profesor)
        {
            codigo = (codigo ?? "").Trim().ToUpperInvariant();
            if (codigo.Length < 3) throw new ArgumentException("Código de curso inválido.");

            Codigo = codigo;
            Nombre = nombre;
            ProfesorAsignado = profesor ?? throw new ArgumentNullException(nameof(profesor));
        }

        public void CambiarProfesor(Profesor nuevo)
        {
            ProfesorAsignado = nuevo ?? throw new ArgumentNullException(nameof(nuevo));
        }

        public override string ToString() => $"{Codigo} - {Nombre} (Prof: {ProfesorAsignado.Nombre})";
    }
}
