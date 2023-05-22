namespace NPDelivery.Domain;

public class Customer
{
    public int Id { get; internal set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Address { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public List<Order> Orders { get; set; }
    public string Remark { get; private set; } = string.Empty;

    private Customer(int id, string name, string surname, string address, string email, string phoneNumber)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public Customer(string name, string surname, string address, string email, string phoneNumber)
    {
        Name = name;
        Surname = surname;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void Update(string name, string surname, string address, string email, string phoneNumber)
    {
        Name = name;
        Surname = surname;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}
