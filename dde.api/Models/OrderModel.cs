using dde.dataaccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dde.api.Models
{
    public class OrderModel
    {
       
        public string orderCode { get; set; }
        public string valeraCode { get; set; }
        public int orderTypeId { get; set; }
        public string orderType { get; set; }
        public string transportCompanyName { get; set; }
        public DateTime orderDate { get; set; }
        public DriverModel driver { get; set; }
        public TruckModel truck { get; set; }

        public Choferes ChoferFactory(DriverModel driver)
        {
            Choferes chofer = new Choferes();
            chofer.NombreCompleto = driver.driverFullName;
            chofer.NumeroDocumentoIdentidad = driver.id;
            return chofer;

        }

        public Vehiculos VehiculoFactory(TruckModel truck)
        {
            Vehiculos vehiculo = new Vehiculos();
            vehiculo.Placa = truck.licensePlate;
            vehiculo.PlacaRastra = truck.licensePlateRaster;
            return vehiculo;
        }

        public EncabezadoPedidos OrderFactory(OrderModel orderModel, Vehiculos truck, Choferes chofer )
        {
            EncabezadoPedidos encabezadoPedidos = new EncabezadoPedidos();
            encabezadoPedidos.NumeroValera = orderModel.valeraCode;
            encabezadoPedidos.NumeroPedido = orderModel.orderCode;
            encabezadoPedidos.VehiculoId = truck.VehiculoId;
            encabezadoPedidos.ChoferId = chofer.ChoferId;
            encabezadoPedidos.TipoOrdenId = orderModel.orderTypeId;
            encabezadoPedidos.NombreTransporte = orderModel.transportCompanyName;
            encabezadoPedidos.FechaPedido = DateTime.Now;

            return encabezadoPedidos;
        }


    }
}
