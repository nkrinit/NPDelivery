namespace NPDelivery.Domain;

public class Product : IHasPrice
{
    public int Id { get; internal set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Price { get; private set; }
    public int StoreId { get; private set; }
    public Store Store { get; private set; }
    public bool IsAvailable { get; private set; } = true;
    public List<OrderedProduct> OrderedProducts { get; private set; }
    public List<Order> Orders { get; private set; }

    private Product(int id, string name, string description, int price, int storeId, bool isAvailable)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        StoreId = storeId;
        IsAvailable = isAvailable;
    }

    public Product(string name, string description, int price, int storeId)
    {
        Name = name;
        Description = description;
        Price = price;
        StoreId = storeId;
    } 

    public void Update(string name, string description, int price, bool isAvailable)
    {
        Name = name;
        Description = description;
        Price = price;
        IsAvailable = isAvailable;
    }
}
