namespace diffieHellmanBazaar.Models;

public class BazaarMessage
{
    public required string Sender { get; set; }

    public string? Recipient { get; set; }

    public string? Message { get; set; }

    public string? Filename { get; set; }


}
