using AutoMapper;
using dde.dataaccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dde.api.Models.Profiles
{
    public class DriverProfile:Profile
    {
        public DriverProfile()
        {
            this.CreateMap<Choferes, DriverModel>()
                .ForMember(destination => destination.id, option => option.MapFrom(source => source.NumeroDocumentoIdentidad))
                .ForMember(destination => destination.driverFullName, option => option.MapFrom(source => source.NombreCompleto))
                .ForMember(destination => destination.driverId, option => option.MapFrom(source => source.ChoferId));
        }
    }
}
