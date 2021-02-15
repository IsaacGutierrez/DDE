using AutoMapper;
using dde.dataaccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dde.api.Models.Profiles
{
    public class TruckProfile:Profile
    {
        public TruckProfile()
        {
            this.CreateMap<Vehiculos, TruckModel>()
                .ForMember(source => source.licensePlate, option => option.MapFrom(destination => destination.Placa))
                .ForMember(source => source.licensePlateRaster, option => option.MapFrom(destination => destination.PlacaRastra))
                .ForMember(source => source.truckId, option => option.MapFrom(destination => destination.VehiculoId));
        }
    }
}
