

namespace Portalclientes1.Servicios
{
    public interface IServicioEncriptacion
    {
        string EncriptacionDocumento(string documento);

        string DesencriptarDocumento(string documento);
    }
}
