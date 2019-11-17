using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portalclientes1.Servicios
{
    public interface IServicioAlmacenamientoAzure
    {
        string AlmacenarEncriptacionAzure(string documento);
    }
}
