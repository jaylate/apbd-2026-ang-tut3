namespace Tutorial3.Entities;

public class Rental {
    public Guid Id { get; init; }
    public Guid RenterId { get; set; }
    public Guid EquipmentId { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ActualReturnDate { get; set; }

    public Rental(Guid renterId, Guid equipmentId)
    {
	RentDate = DateTime.Now;
    }
}
