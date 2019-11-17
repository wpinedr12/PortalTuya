using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalWeb.Models
{
    public class Producto

    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Tipo { get; set; }
    }
}
