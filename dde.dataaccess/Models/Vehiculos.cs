using System;
using System.Collections.Generic;

namespace dde.dataaccess.Models
{
    public partial class Vehiculos
    {
        public Vehiculos()
        {
            EncabezadoPedidos = new HashSet<EncabezadoPedidos>();
        }

        public int VehiculoId { get; set; }
        public string Placa { get; set; }
        public string PlacaRastra { get; set; }

        public ICollection<EncabezadoPedidos> EncabezadoPedidos { get; set; }
    }
}
