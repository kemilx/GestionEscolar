namespace GestionEscolar.Models
{
    public class Admin : Usuario
    {
        public Admin(string id, string nombre) : base(id, nombre) { }

        public override string MostrarPerfil()
        {
            return $"[ADMIN]\nNombre: {Nombre}\nID: {Id}\nRol: Administrador del sistema";
        }
    }
}
