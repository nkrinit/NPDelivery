namespace NPDelivery.Domain;

public class PaymentInfo
{
    public int Id { get; internal set; }
    public string CardNumber { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    // Securiry code should be added manually
}
