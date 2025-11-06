using diffieHellmanBazaar.Models;

namespace diffieHellmanBazaar.Services;

public class BazaarService
{
    private readonly ILogger<BazaarService> _logger;

    public event Action<BazaarMessage>? OnMessageReceived;

    public BazaarService(ILogger<BazaarService> logger)
    {
        _logger = logger;
    }

    public void SendMessage(BazaarMessage message)
    {
        _logger.LogInformation("Received message from {Sender}: {Message}", message.Sender, message.Message);
        OnMessageReceived?.Invoke(message);
    }
}
