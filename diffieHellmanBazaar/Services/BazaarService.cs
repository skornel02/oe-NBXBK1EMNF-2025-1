using diffieHellmanBazaar.Models;

namespace diffieHellmanBazaar.Services;

/// <summary>
/// Provides functionality for sending messages within the Bazaar system and notifying subscribers when a message is
/// received.
/// </summary>
/// <remarks>BazaarService exposes an event for message reception, allowing consumers to react to incoming
/// messages. This service is typically used to facilitate communication between components in the Bazaar domain.
/// Instances of BazaarService require an ILogger for logging message activity.</remarks>
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
