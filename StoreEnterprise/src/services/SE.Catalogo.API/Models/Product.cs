using SE.Core.DomainObjects;

namespace SE.Catalogo.API.Models
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Value { get; set; }
        public DateTime DateRegister { get; set; }
        public string Image { get; set; }
        public int StockQuantity { get; set; }
    }
}
