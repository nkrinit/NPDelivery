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
    public int? ShiftId { get; private set; }
    //Probably autofill from Store/Customer info
    public string From { get; private set; }
    public string To { get; private set; }

    private Order(int id, int customerId, int storeId, int? courierId, int? shiftId, string from, string to)
    {
        Id = id;
        CustomerId = customerId;
        StoreId = storeId;
        CourierId = courierId;
        ShiftId = shiftId;
        From = from;
        To = to;
    }

    public Order(int customerId, int storeId, string from, string to)
    {
        CustomerId = customerId;
        StoreId = storeId;
        OrderedProducts = new List<OrderedProduct>();
        From = from;
        To = to;
    }

    public void AddProduct(int productId, int quantity)
    {
        var orderedProduct = OrderedProducts.FirstOrDefault(op => op.ProductId == productId);
        if(orderedProduct == null)
        {
            OrderedProducts.Add(new OrderedProduct(this, productId, quantity));
        }
        else
        {
            orderedProduct.UpdateQuantity(orderedProduct.Quantity + quantity);
        }
    }

    public void AddProduct(Product product, int quantity)
    {
        var orderedProduct = OrderedProducts.FirstOrDefault(op => op.Product == product);
        if (orderedProduct == null)
        {
            OrderedProducts.Add(new OrderedProduct(this, product, quantity));
        }
        else
        {
            orderedProduct.UpdateQuantity(orderedProduct.Quantity + quantity);
        }
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

    public OrderedProduct(Order order, int productId, int quantity)
    {
        Order = order;
        ProductId = productId;
        Quantity = quantity;
    }

    public OrderedProduct(Order order, Product product, int quantity)
    {
        Order = order;
        Product = product;
        Quantity = quantity;
    }

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }
}