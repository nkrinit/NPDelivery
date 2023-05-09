namespace NPDelivery.Domain;

public class Store
{
    public int Id { get; internal set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public List<Product> Products { get; private set; }
    public int StoreKeeperId { get; private set; }
    public StoreKeeper StoreKeeper { get; private set; }
    public string Address { get; private set; }
    public int Score { get; private set; } = 0;

    private Store(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public Store(string name, string description, int storeKeeperId, string address)
    {
        Name = name;
        Description = description;
        StoreKeeperId = storeKeeperId;
        Address = address;
    }
    public void Update(string name, string description, string address)
    {
        Name = name;
        Description = description;
        Address = address;
    }
}

public class StoreKeeper
{
    public int Id { get; internal set; }
    public List<Store> Stores { get; private set; }
}