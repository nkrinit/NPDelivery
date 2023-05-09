namespace NPDelivery.Domain;

public class Order
{
    // public Guid ExternalId { get; private set; }
    public int Id { get; internal set; }
    public int CustomerId { get; private set; }
    public int StoreId { get; private set; }
    public List<OrderedProduct> OrderedProducts { get; private set; }
    public List<Product> Products { get; private set; }
    public int? CourierId { get; private set; }
    public string From { get; private set; }
    public string To { get; private set; }

    private Order(int id, int customerId, int storeId, int? courierId)
    {
        Id = id;
        CustomerId = customerId;
        StoreId = storeId;
        CourierId = courierId;
    }

    public Order(int customerId, int storeId, List<OrderedProduct> orderedProducts, string from, string to)
    {
        CustomerId = customerId;
        StoreId = storeId;
        OrderedProducts = orderedProducts;
        From = from;
        To = to;
    }
}

public class OrderedProduct
{
    public int OrderId { get; private set; }
    public Order Order { get; private set; } = null!;
    public int ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    public int Quantity { get; private set; }

    public OrderedProduct(int orderId, int productId, int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }
}