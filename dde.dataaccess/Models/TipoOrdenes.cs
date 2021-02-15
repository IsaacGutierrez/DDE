using System;
using System.Collections.Generic;

namespace dde.dataaccess.Models
{
    public partial class TipoOrdenes
    {
        public TipoOrdenes()
        {
            EncabezadoPedidos = new HashSet<EncabezadoPedidos>();
        }

        public int TipoOrdenId { get; set; }
        public string Nombre { get; set; }

        public ICollection<EncabezadoPedidos> EncabezadoPedidos { get; set; }
    }
}
