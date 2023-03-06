namespace BankAPI.Models
{
    public class Order
    {
        // [ReadOnly(true)]
        public int Id { get; set; }
        public uint CardNumber { get; set; }
        public uint PayAmount { get; set; }
        public ushort StatusCode { get; set; }
    }

}