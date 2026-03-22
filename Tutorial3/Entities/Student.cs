namespace Tutorial3.Entities;

public class Student : User
{
    public override int MaxRentals => RentalPolicy.MAX_RENTAL_FOR_STUDENT;

    public Student(string firstName, string lastName)
    : base(firstName, lastName) { }
}
