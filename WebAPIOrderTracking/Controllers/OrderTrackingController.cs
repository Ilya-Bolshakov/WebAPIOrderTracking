using Microsoft.AspNetCore.Mvc;
using WebAPIOrderTracking.Models;
using WebAPIOrderTracking;
using Microsoft.AspNetCore.Authorization;

namespace OrderTrackingWebAPI.Controllers
{
    [ApiController]
    [Route("api/OrderTracking")]
    public class OrderTrackingController : ControllerBase
    {
        private readonly List<Order> orders;

        private readonly ILogger<OrderTrackingController> _logger;

        public OrderTrackingController(ILogger<OrderTrackingController> logger)
        {
            _logger = logger;
            orders = new List<Order>();
            orders.Add(
                new Order { ID = 0, FirstName = "aa", LastName = "rnd1", VisitDate = new DateTime(), NameOrder = "order1", Description = "desc" }
            );
            orders.Add(
                new Order { ID = 1, FirstName = "bb", LastName = "rnd2", VisitDate = new DateTime(2022, 5, 1), NameOrder = "order2", Description = "desc" }
            );
            orders.Add(
                new Order { ID = 2, FirstName = "aa", LastName = "rnd1", VisitDate = new DateTime(), NameOrder = "order1", Description = "desc" }
            );
            orders.Add(
                new Order { ID = 3, FirstName = "aa", LastName = "rnd1", VisitDate = new DateTime(), NameOrder = "order1", Description = "desc" }
            );
            orders.Add(
                new Order { ID = 4, FirstName = "aa", LastName = "rnd1", VisitDate = new DateTime(), NameOrder = "order1", Description = "desc" }
            );
            orders.Add(
                new Order { ID = 5, FirstName = "aa", LastName = "rnd1", VisitDate = new DateTime(), NameOrder = "order1", Description = "desc" }
            );
            orders.Add(
                new Order { ID = 6, FirstName = "aa", LastName = "rnd1", VisitDate = new DateTime(), NameOrder = "order1", Description = "desc" }
            );
            orders.Add(
                new Order { ID = 7, FirstName = "aa", LastName = "rnd1", VisitDate = new DateTime(), NameOrder = "order1", Description = "desc" }
            );
            orders.Add(
                new Order { ID = 8, FirstName = "aa", LastName = "rnd1", VisitDate = new DateTime(), NameOrder = "order1", Description = "desc" }
            );
            orders.Add(
                new Order { ID = 9, FirstName = "aa", LastName = "rnd1", VisitDate = new DateTime(), NameOrder = "order1", Description = "desc" }
            );
            orders.Add(
                new Order { ID = 10, FirstName = "aa", LastName = "rnd1", VisitDate = new DateTime(), NameOrder = "order1", Description = "desc" }
            );
        }

        [HttpPost]
        [Route("GetOrders")]
        [Authorize]
        public IEnumerable<Order> GetOrders([FromBody] FilterModel filterModel)
        {
            return orders;
        }


        [HttpGet]
        [Authorize]
        [Route("GetOrder/{id}")]
        public Order GetOrder(int id)
        {
            return orders.FirstOrDefault(item => item.ID == id);
        }
    
    }
}
