using System;
using System.Collections.Generic;

namespace dde.dataaccess.Models
{
    public partial class CapacitacionMultimedia
    {
        public int CapacitacionMultimediaId { get; set; }
        public int? EncabezadoPedidoId { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
        public int? ProgramacionMultimediaId { get; set; }

        public EncabezadoPedidos EncabezadoPedido { get; set; }
        public ProgramacionMultimedia ProgramacionMultimedia { get; set; }
    }
}
