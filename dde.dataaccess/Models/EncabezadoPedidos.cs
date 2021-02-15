using System;
using System.Collections.Generic;

namespace dde.dataaccess.Models
{
    public partial class EncabezadoPedidos
    {
        public EncabezadoPedidos()
        {
            CapacitacionMultimedia = new HashSet<CapacitacionMultimedia>();
        }

        public int EncabezadoPedidoId { get; set; }
        public string NumeroPedido { get; set; }
        public int? TipoOrdenId { get; set; }
        public string NumeroValera { get; set; }
        public DateTime? FechaPedido { get; set; }
        public int? ChoferId { get; set; }
        public int? VehiculoId { get; set; }
        public string NombreTransporte { get; set; }

        public Choferes Chofer { get; set; }
        public TipoOrdenes TipoOrden { get; set; }
        public Vehiculos Vehiculo { get; set; }
        public ICollection<CapacitacionMultimedia> CapacitacionMultimedia { get; set; }
    }
}
