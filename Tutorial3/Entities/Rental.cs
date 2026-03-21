namespace Tutorial3.Entities;

public class Rental {
    public Guid Id { get; init; }
    public User Renter { get; set; }
    public Equipment RentedEquipment { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }

    public int Penalty { get; set; }

    public Rental(User user, Equipment equipment, DateTime dueDate)
    {
	Renter = user;
	RentedEquipment = equipment;
	RentDate = DateTime.Now;
	DueDate = dueDate;
	ActualReturnDate = null;
	Penalty = 0;
    }
}
