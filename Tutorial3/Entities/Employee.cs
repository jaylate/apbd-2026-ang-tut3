namespace Tutorial3.Entities;

public class Employee : User
{
    public override int MaxRentals => RentalPolicy.MAX_RENTAL_FOR_EMPLOYEE;

    public Employee(string firstName, string lastName)
    : base(firstName, lastName) { }
}
