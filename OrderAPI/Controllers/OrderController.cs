using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Nest;
using OrderAPI.Models;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _ordersCollections;
        private readonly ILogger<OrderController> _logger;
        
        public OrderController(ILogger<OrderController> logger)
        {
            var dbhost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbname = Environment.GetEnvironmentVariable("DB_NAME");

            var connectionstring = $"mongodb://{dbhost}:27017/{dbname}";

            var mongourl = MongoUrl.Create(connectionstring) ;
            var mongoclient = new MongoClient(mongourl);
            var database = mongoclient.GetDatabase(mongourl.DatabaseName) ;
            _ordersCollections = database.GetCollection<Order>("order");
            _logger = logger;
            
        }


        //[HttpGet("{keyword}", Name="GetELKSearch")]
        //public async Task<IActionResult> GetOrderSearchByIndex(string keyword)
        //{
        //    var results = await _elasticClient.SearchAsync<Order>(s => s.Query(
        //            q => q.QueryString(
        //                d => d.Query('*' + keyword + '*')
        //        )
        //           ).Size(1000));

        //    return Ok(results);
        //}



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            _logger.LogInformation("This is a Welcome Message from GetOrder Details");
            return await _ordersCollections.Find(Builders<Order>.Filter.Empty).ToListAsync();
           

        }

        [HttpGet("{orderid}")]
        public async Task<ActionResult<Order>> GetOrderById(string orderid)
        {
            _logger.LogInformation("Seri Log is Working");
            var filterData = Builders<Order>.Filter.Eq(x=>x.OrderId, orderid);
            return await _ordersCollections.Find(filterData).SingleOrDefaultAsync();

        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            _logger.LogInformation("This is a Welcome Message from PostOrder Details");
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
