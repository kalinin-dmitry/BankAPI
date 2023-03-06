using Microsoft.EntityFrameworkCore;

namespace BankAPI.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
    }

    public class OrderContextFactory
    {
        //private readonly string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=OrdersDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        private readonly IConfiguration _config;
        public OrderContextFactory(IConfiguration config)
        {
            _config = config;
        }
        public OrderContext Create()
        {
            var options = new DbContextOptionsBuilder<OrderContext>().UseSqlServer(_config.GetConnectionString("DefaultConnection")).Options;
            //var options = new DbContextOptionsBuilder<OrderContext>().UseSqlServer(_connectionString).Options;
            return new OrderContext(options);
        }
    }
}
