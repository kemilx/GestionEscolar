using System;

namespace GestionEscolar.Models
{
    public class Profesor : Usuario
    {
        private string _departamento;
        public string Departamento
        {
            get => _departamento;
            private set
            {
                value = (value ?? "").Trim();
                if (value.Length < 3) throw new ArgumentException("Departamento inválido.");
                _departamento = value;
            }
        }

        public Profesor(string id, string nombre, string departamento)
            : base(id, nombre)
        {
            Departamento = departamento;
        }

        public override string MostrarPerfil()
        {
            return $"[PROFESOR]\nNombre: {Nombre}\nID: {Id}\nDepartamento: {Departamento}";
        }
    }
}
