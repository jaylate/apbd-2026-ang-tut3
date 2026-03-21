namespace Tutorial3.Services;

using Tutorial3.Entities;

interface IRentalService {
    public void AddUser(User user);
    public void AddEquipment(Equipment e);
    public void MarkEquipmentUnavailable(Equipment e);

    public Rental Rent(User user, Equipment e, DateTime dueDate);
    public void Return(Equipment e);

    public List<Equipment> GetAllEquipment();
    public List<User> GetAllUsers();
    public List<Rental> GetRentalsForUser(User user);
    public List<Rental> GetOverdueRentals();
    public List<Equipment> GetAvailableEquipment();
    public void RentalServiceStateSummary();
}
