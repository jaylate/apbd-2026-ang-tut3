namespace Tutorial3.Services;

using Tutorial3.Entities;

public class ReportService {
    private readonly IRentalService _rentalService;

    public ReportService(IRentalService rentalService)
    {
	_rentalService = rentalService;
    }

    public RentalSummary GenerateSummary()
    {
	var equipment = _rentalService.GetAllEquipment();
        var users = _rentalService.GetAllUsers();
        var rentals = _rentalService.GetAllRentals();

        var activeRentals = rentals.Where(r => r.ActualReturnDate == null).ToList();
        var overdueRentals = activeRentals.Where(r => r.DueDate < DateTime.Now).ToList();

        return new RentalSummary
        {
            TotalEquipment = equipment.Count,
            AvailableEquipment = equipment.Count(e => e.IsAvailable),
            TotalUsers = users.Count,
            TotalRentals = rentals.Count,
            ActiveRentals = activeRentals.Count,
            OverdueRentals = overdueRentals.Count,
            StudentCount = users.OfType<Student>().Count(),
            EmployeeCount = users.OfType<Employee>().Count()
        };
    }
}
