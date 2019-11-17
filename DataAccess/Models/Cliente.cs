using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Cliente
     {

        public int Id { get; set; }

        public string DocumentoIdentidad { get; set; }

        public string TipoDocumento { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}
