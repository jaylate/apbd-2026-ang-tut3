namespace Tutorial3.Exceptions;

public class EquipmentNotFoundException : RentalException
{
    public Guid EquipmentId { get; }

    public EquipmentNotFoundException(Guid equipmentId)
        : base($"Equipment with ID {equipmentId} not found")
    {
        EquipmentId = equipmentId;
    }
}
