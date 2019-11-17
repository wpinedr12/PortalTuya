using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProductos.Models
{
    public class CrearProducto
    {
        public string TipoProducto { get; set; }

        public int ClienteId { get; set; }

        public string UrlRetorno { get; set; }
    }
}
