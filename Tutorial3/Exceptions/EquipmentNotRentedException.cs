namespace Tutorial3.Exceptions;

public class EquipmentNotRentedException : RentalException
{
    public Guid EquipmentId { get; }
    
    public EquipmentNotRentedException(Guid equipmentId) 
        : base($"Equipment with ID {equipmentId} has not been rented")
    {
        EquipmentId = equipmentId;
    }
}
