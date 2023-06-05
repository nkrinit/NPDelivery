namespace NPDelivery.Domain;

public class Shift
{
    public int Id { get; internal set; }
    public int CourierId { get; internal set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public List<Order> Orders { get; private set; }
}