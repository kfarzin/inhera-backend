namespace Inhera.Shared.Models.DomainEvents
{
    public record AccountRegistrationDomainEvent
    {
        public required string Email { get; init; }
        public required string Code { get; init; }
    }
}