using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле 'CardNumber' является обязательным.")]
        [Range(10000, 99999, ErrorMessage = "Значение поля 'CardNumber' должно быть от 10000 до 99999.")]
        public uint CardNumber { get; set; }
        [Required(ErrorMessage = "Поле 'PayAmount' является обязательным.")]
        [Range(0, 100000, ErrorMessage = "Значение поля 'PayAmount' должно быть от 0 до 100000.")]
        public uint PayAmount { get; set; }
        public ushort StatusCode { get; set; }
    }

}