namespace Insurance.Domain.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float SalesPrice { get; set; }
        public int ProductTypeId { get; set; }
        public string? ProductTypeName { get; set; }
        public bool CanBeInsured { get; set; }
    }
}
