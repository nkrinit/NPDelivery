using System.Net;

namespace NPDelivery.Domain;

public class Courier
{
    public int Id { get; internal set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public List<Shift> Shifts { get; private set; }
    public List<Order> Orders { get; set; }
    public int Score { get; private set; }
    public int Remark { get; private set; }


    private Courier(int id, string name, string surname, string email, string phone)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
    }

    public Courier(string name, string surname, string email, string phone)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
    }

    public void Update(string name, string surname, string email, string phone)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
    }

}
