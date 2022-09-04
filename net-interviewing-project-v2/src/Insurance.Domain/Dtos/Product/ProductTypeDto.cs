namespace Insurance.Domain.Dtos.Product
{
    public class ProductTypeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool CanBeInsured { get; set; }
    }
}
