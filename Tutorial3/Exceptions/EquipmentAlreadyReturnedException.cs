namespace Tutorial3.Exceptions;

public class EquipmentAlreadyReturnedException : RentalException
{
    public Guid EquipmentId { get; }

    public EquipmentAlreadyReturnedException(Guid equipmentId)
        : base($"Equipment with ID {equipmentId} has already been returned")
    {
        EquipmentId = equipmentId;
    }
}
