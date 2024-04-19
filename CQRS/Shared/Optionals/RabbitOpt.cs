namespace CQRS.Shared.Optionals
{
    public sealed class RabbitOpt
    {
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RabbitQueue QueueAddUser { get; set; }
    }

    public sealed class RabbitQueue
    {
        public string Name { get; set; }
        public int BatchSize { get; set; }
        public bool Durable { get; set; }

        public Uri GetSendEndpoint()
        {
            if (Durable)
            {
                return new Uri($"queue:{Name}");
            }
            else
            {
                return new Uri($"queue:{Name}?durable=false");
            }
        }

    }
}
