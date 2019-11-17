using PortalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalWeb.ViewModels
{
    public class AsociarProductoViewModel
    {
        public Cliente Cliente { get; set; }

        public string TipoProducto { get; set; }

        public IEnumerable<Producto> Productos { get; set; }

    }
}
