namespace GestionEscolar.Services.Reportes
{
    public interface IReporte
    {
        string Nombre { get; }
        string Generar();
    }
}
