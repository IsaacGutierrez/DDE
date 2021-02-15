using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dde.api.Models;
using dde.dataaccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dde.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private DDEContext _entities;
        private IMapper _mapper;
        public OrderController(DDEContext _entities, IMapper mapper)
        {
            this._entities = _entities;
            this._mapper = mapper;
        }
       [HttpPost]
       public IActionResult PostOrders(OrderModel model)
       {
            try
            {
                var orderExist = _entities
               .EncabezadoPedidos
               .Where(order => order.NumeroPedido.ToLower() == model.orderCode.ToLower())
               .FirstOrDefault();

                if (orderExist == null)
                {
                    var truck = model.VehiculoFactory(model.truck);
                    _entities.Vehiculos.Add(truck);
                    _entities.SaveChanges();

                    var driverRecord = _entities
                        .Choferes
                        .Where(driver => driver.NumeroDocumentoIdentidad.ToLower() == model.driver.id.ToLower())
                        .FirstOrDefault();

                    if (driverRecord == null)
                    {
                        driverRecord = model.ChoferFactory(model.driver);

                        _entities.Choferes.Add(driverRecord);
                        _entities.SaveChanges();
                    }


                    var order = model.OrderFactory(model, truck, driverRecord);
                    _entities.EncabezadoPedidos.Add(order);
                    _entities.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var orders = _entities
                .EncabezadoPedidos
                .Include(model => model.Chofer)
                .Include(model => model.Vehiculo)
                .Include(model => model.TipoOrden)
                .ToList();

             return  Ok(_mapper.Map<OrderModel[]>(orders));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

      
    }
}