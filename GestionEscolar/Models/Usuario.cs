using System;

namespace GestionEscolar.Models
{
    public abstract class Usuario
    {
        public string Id { get; }

        private string _nombre;
        public string Nombre
        {
            get => _nombre;
            protected set
            {
                value = (value ?? "").Trim();
                if (value.Length < 2) throw new ArgumentException("Nombre inválido.");
                _nombre = value;
            }
        }

        protected Usuario(string id, string nombre)
        {
            id = (id ?? "").Trim().ToUpperInvariant();
            if (id.Length < 2) throw new ArgumentException("Id inválido.");
            Id = id;
            Nombre = nombre;
        }

        public abstract string MostrarPerfil();

        public override string ToString() => $"{Nombre} ({Id})";
    }
}
