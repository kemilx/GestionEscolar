using System;

namespace GestionEscolar.Models
{
    public class Estudiante : Usuario
    {
        private string _matricula;
        public string Matricula
        {
            get => _matricula;
            private set
            {
                value = (value ?? "").Trim().ToUpperInvariant();
                if (value.Length < 4) throw new ArgumentException("Matrícula inválida.");
                _matricula = value;
            }
        }

        public Estudiante(string id, string nombre, string matricula)
            : base(id, nombre)
        {
            Matricula = matricula;
        }

        public override string MostrarPerfil()
        {
            return $"[ESTUDIANTE]\nNombre: {Nombre}\nID: {Id}\nMatrícula: {Matricula}";
        }
    }
}
