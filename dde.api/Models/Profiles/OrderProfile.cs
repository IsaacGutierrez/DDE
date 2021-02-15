using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dde.dataaccess.Models;

namespace dde.api.Models.Profiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            this.CreateMap<EncabezadoPedidos, OrderModel>()
                .ForMember(destination => destination.orderCode, option => option.MapFrom(source => source.NumeroPedido))
                .ForMember(destination => destination.valeraCode, option => option.MapFrom(source => source.NumeroValera))
                .ForMember(destination => destination.orderDate, option => option.MapFrom(source => source.FechaPedido))
                .ForMember(destination => destination.orderType, option => option.MapFrom(source => source.TipoOrden.Nombre))
                .ForMember(destination => destination.orderTypeId, option => option.MapFrom(source => source.TipoOrden.TipoOrdenId))
                .ForMember(destination => destination.truck, option => option.MapFrom(source => source.Vehiculo))
                .ForMember(destination => destination.driver, option => option.MapFrom(source => source.Chofer))
                .ForMember(destination => destination.transportCompanyName, option => option.MapFrom(source => source.NombreTransporte));
        }
    }
}
