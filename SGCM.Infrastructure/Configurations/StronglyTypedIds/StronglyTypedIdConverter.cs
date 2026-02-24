using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SGCM.Infrastructure.Configurations;

public sealed class StronglyTypedIdConverter<TId> : ValueConverter<TId, Guid>
    where TId : struct
{
    public StronglyTypedIdConverter(Func<Guid, TId> fromGuid)
        : base(
            id => GetValue(id),
            guid => fromGuid(guid))
    { }

    private static Guid GetValue(TId id)
    {
        var prop = typeof(TId).GetProperty("Value")
                   ?? throw new InvalidOperationException($"{typeof(TId).Name} must have a 'Value' property of type Guid.");

        return (Guid)prop.GetValue(id)!;
    }
}