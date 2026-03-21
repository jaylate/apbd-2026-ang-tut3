namespace Tutorial3.Exceptions;

public class EquipmentUnavailableException : RentalException
{
    public Guid EquipmentId { get; }

    public EquipmentUnavailableException(Guid equipmentId)
        : base($"Equipment with ID {equipmentId} is not available for rental")
    {
        EquipmentId = equipmentId;
    }
}
