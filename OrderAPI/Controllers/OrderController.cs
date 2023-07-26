using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrderAPI.Models;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _ordersCollections;
        public OrderController()
        {
            var dbhost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbname = Environment.GetEnvironmentVariable("DB_NAME");

            var connectionstring = $"mongodb://{dbhost}:27017/{dbname}";

            var mongourl = MongoUrl.Create(connectionstring) ;
            var mongoclient = new MongoClient(mongourl);
            var database = mongoclient.GetDatabase(mongourl.DatabaseName) ;
            _ordersCollections = database.GetCollection<Order>("order");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _ordersCollections.Find(Builders<Order>.Filter.Empty).ToListAsync();

        }

        [HttpGet("{orderid}")]
        public async Task<ActionResult<Order>> GetOrderById(string orderid)
        {
            var filterData = Builders<Order>.Filter.Eq(x=>x.OrderId, orderid);
            return await _ordersCollections.Find(filterData).SingleOrDefaultAsync();

        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            await _ordersCollections.InsertOneAsync(order);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Updateorder(Order order)
        {
            var filterData = Builders<Order>.Filter.Eq(x => x.OrderId, order.OrderId);
            await _ordersCollections.ReplaceOneAsync(filterData,order);
            
            return Ok();
        }

        [HttpDelete("{orderid}")]
        public async Task<ActionResult<Order>> DeleteOrder(string orderid)
        {
            var filterData = Builders<Order>.Filter.Eq(x => x.OrderId, orderid);
            await _ordersCollections.DeleteOneAsync(filterData);
            return Ok();
        }
    }
}
