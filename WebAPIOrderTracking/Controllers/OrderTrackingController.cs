using Microsoft.AspNetCore.Mvc;
using WebAPIOrderTracking.Models;
using WebAPIOrderTracking;
using Microsoft.AspNetCore.Authorization;
using WebAPIOrderTracking.Models.Entities;
using System.Threading;

namespace OrderTrackingWebAPI.Controllers
{
    [ApiController]
    [Route("api/OrderTracking")]
    public class OrderTrackingController : ControllerBase
    {
        private readonly ILogger<OrderTrackingController> _logger;
        private readonly OrderTrackingContext _context;

        public OrderTrackingController(ILogger<OrderTrackingController> logger, OrderTrackingContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Route("GetOrders")]
        [Authorize]
        public IEnumerable<Order> GetOrders([FromBody] FilterModel filterModel)
        {
            return _context.Orders.ToList();
        }


        [HttpGet]
        [Authorize]
        [Route("GetOrder/{id}")]
        public Order GetOrder(int id)
        {
            return _context.Orders.FirstOrDefault(item => item.Orderid == id);
        }

        [HttpPost]
        [Authorize]
        [Route("AddOrder")]
        public IActionResult AddOrder([FromBody]Order order)
        {
            try
            {
                order.Updatedate = DateTime.Now;
                TimeZoneInfo cstZone = TimeZoneInfo.Local;
                order.Visitdate = TimeZoneInfo.ConvertTime(order.Visitdate, cstZone);
                _context.Orders.Add(order);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Conflict(new { error = ex.Message });
            }
            
        }

        [HttpPut]
        [Authorize]
        [Route("EditOrder")]
        public IActionResult EditOrder([FromBody]Order order)
        {
            try
            {
                order.Updatedate = DateTime.Now;
                _context.Orders.Update(order);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                var orderForRemove = _context.Orders.FirstOrDefault(i => i.Orderid == id);
                if (orderForRemove is Order o) 
                {
                    _context.Orders.Remove(o);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    throw new Exception("Заказа не существует");
                }
            }
            catch (Exception ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }
    }
}
