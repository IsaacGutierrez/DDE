using System;
using System.Collections.Generic;

namespace dde.dataaccess.Models
{
    public partial class Choferes
    {
        public Choferes()
        {
            EncabezadoPedidos = new HashSet<EncabezadoPedidos>();
        }

        public int ChoferId { get; set; }
        public string NombreCompleto { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }

        public ICollection<EncabezadoPedidos> EncabezadoPedidos { get; set; }
    }
}
