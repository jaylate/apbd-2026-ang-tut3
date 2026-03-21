namespace Tutorial3.Exceptions;

public class RentalLimitExceededException : RentalException
{
    public Guid UserId { get; }
    public int MaxRentals { get; }

    public RentalLimitExceededException(Guid userId, int maxRentals)
        : base($"User with ID {userId} has exceeded the maximum number of rentals ({maxRentals})")
    {
        UserId = userId;
        MaxRentals = maxRentals;
    }
}
