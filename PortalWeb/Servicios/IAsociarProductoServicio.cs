using PortalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalWeb.Servicios
{
    public interface IAsociarProductoServicio
    {
        Task<IEnumerable<Producto>> ObtenerProductos(int idCliente);
    }
}
