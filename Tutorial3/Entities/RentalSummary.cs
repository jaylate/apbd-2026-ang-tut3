namespace Tutorial3.Entities;

public class RentalSummary
{
    public int TotalEquipment { get; init; }
    public int AvailableEquipment { get; init; }
    public int RentedEquipment { get; init; }
    public int TotalUsers { get; init; }
    public int TotalRentals { get; init; }
    public int ActiveRentals { get; init; }
    public int OverdueRentals { get; init; }
    public int StudentCount { get; init; }
    public int EmployeeCount { get; init; }
}
