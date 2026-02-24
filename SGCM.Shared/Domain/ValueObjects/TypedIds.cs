namespace SGCM.Shared.Domain.ValueObjects;

public readonly record struct DoctorId(Guid Value) {
    public static DoctorId New() => new(Guid.CreateVersion7());
    public static DoctorId From(Guid value) => new(value);
    public override string ToString() => Value.ToString();
}

public readonly record struct PatientId(Guid Value)
{
    public static PatientId New() => new(Guid.CreateVersion7());
    public static PatientId From(Guid value) => new(value);
    public override string ToString() => Value.ToString();
}