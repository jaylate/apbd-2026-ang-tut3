namespace Tutorial3.Services;

using Tutorial3.Entities;
using Tutorial3.Exceptions;

public class RentalService : IRentalService {
   List<Equipment> equipment = new();
   List<User> users = new();
   List<Rental> rentals = new();

   public void AddUser(User user)
   {
	users.Add(user);
   }

   public void AddEquipment(Equipment e)
   {
	equipment.Add(e);
   }

   public void MarkEquipmentUnavailable(Equipment e)
   {
	var eq = equipment.FirstOrDefault(x => x.Id == e.Id);
	if (eq != null)
	    eq.IsAvailable = false;
   }

   public Rental Rent(User user, Equipment e, DateTime dueDate) {
	var u = users.FirstOrDefault(x => x.Id == user.Id);
	if (u == null)
	    throw new UserNotFoundException(user.Id);

	var userRentQuantity = rentals.Count(r => r.Renter.Id == user.Id && r.ActualReturnDate == null);

	if (user is Student && userRentQuantity >= RentalPolicy.MAX_RENTAL_FOR_STUDENT)
	    throw new RentalLimitExceededException(user.Id, RentalPolicy.MAX_RENTAL_FOR_STUDENT);
	if (user is Employee && userRentQuantity >= RentalPolicy.MAX_RENTAL_FOR_EMPLOYEE)
	    throw new RentalLimitExceededException(user.Id, RentalPolicy.MAX_RENTAL_FOR_EMPLOYEE);

	var eq = equipment.FirstOrDefault(x => x.Id == e.Id);
	if (eq == null)
	    throw new EquipmentNotFoundException(e.Id);
	if (!eq.IsAvailable)
	    throw new EquipmentUnavailableException(e.Id);

	eq.IsAvailable = false;
	Rental rent = new Rental(user, eq, dueDate);
	rentals.Add(rent);
	return rent;
    }

   public void Return(Equipment e) {
	var r = rentals.FirstOrDefault(x => x.RentedEquipment.Id == e.Id);
	if (r == null)
	    throw new EquipmentNotRentedException(e.Id);
	if (r.ActualReturnDate != null)
	    throw new EquipmentAlreadyReturnedException(e.Id);

	r.ActualReturnDate = DateTime.Now;
	if (r.ActualReturnDate > r.DueDate) {
	    int daysLate = (r.ActualReturnDate!.Value - r.DueDate).Days;
	    r.Penalty = daysLate * RentalPolicy.OVERDUE_PENALTY_PER_DAY;
	}
	r.RentedEquipment.IsAvailable = true;
    }

   public List<Equipment> GetAllEquipment()
   {
	return equipment;
   }

   public List<User> GetAllUsers()
   {
        return users;
   }

   public List<Rental> GetAllRentals()
   {
	return rentals;
   }

   public List<Rental> GetRentalsForUser(User user)
   {
	return rentals.Where(r => r.Renter.Id == user.Id).ToList();
   }

   public List<Rental> GetOverdueRentals()
   {
	return rentals.Where(r => r.ActualReturnDate == null && r.DueDate < DateTime.Now).ToList();
   }

   public List<Equipment> GetAvailableEquipment()
   {
	return equipment.Where(e => e.IsAvailable).ToList();
   }
}
