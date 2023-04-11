namespace NPDelivery;

/// <summary>
/// A DB wrapper for initial testing 
/// </summary>
public class Database
{
    private int _nextOrderId = 1;
    public List<Order> Orders { get; set; } = new List<Order>();

    public void AddOrder(Order order)
    {
        order.Id = _nextOrderId++;
        Orders.Add(order);
    }
}

// to do: move to a separate file(under the Domain folder?)
// to do: left only customer data + store data + items. Consider adding a separate Menu feature?
public class Order
{
    public int Id { get; internal set; }
    public int CustomerId { get; }
    public int StoreId { get; }
    public int MenuItemId { get; }
    public int Quantity { get; }

    public Order(int customerId, int storeId, int menuItemId, int quantity)
    {
        CustomerId = customerId;
        StoreId = storeId;
        MenuItemId = menuItemId;
        Quantity = quantity;
    }
}
