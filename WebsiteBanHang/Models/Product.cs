using System.Collections.Generic;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }

    // Upload
    public string? ImageUrl { get; set; }
}