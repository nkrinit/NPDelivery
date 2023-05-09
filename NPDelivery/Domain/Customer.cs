namespace NPDelivery.Domain;

public class Customer
{
    public int Id { get; internal set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Address { get; private set; }

    private Customer(int id, string name, string surname, string address)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Address = address;
    }

    public Customer(string name, string surname, string address)
    {
        Name = name;
        Surname = surname;
        Address = address;
    }

    public void Update(string name, string surname, string address)
    {
        Name = name;
        Surname = surname;
        Address = address;
    }
}
