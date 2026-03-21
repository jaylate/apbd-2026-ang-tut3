namespace Tutorial3.ui;

using Tutorial3.Services;
using Tutorial3.Entities;
using Tutorial3.Exceptions;

public class Menu
{
    private readonly IRentalService _rentalService;
    private readonly IReportService _reportService;

    public Menu(IRentalService rentalService, IReportService reportService)
    {
        _rentalService = rentalService;
        _reportService = reportService;
    }

    private int ReadChoice(int max, int defaultChoice = 1)
    {
        Console.Write("Enter choice [" + defaultChoice + "]: ");
        while (true)
        {
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                return defaultChoice;
            }

            if (int.TryParse(input, out int choice) && choice >= 0 && choice <= max)
            {
                return choice;
            }
            Console.Write("Invalid. Try again: ");
        }
    }

    private int ShowCustomMenu(string prompt, string[] choices, bool isFirstMenu = false)
    {
        Console.WriteLine(prompt);

        for (int i = 1; i <= choices.Length; i += 1)
        {
            Console.WriteLine("[" + i + "] " + choices[i - 1]);
        }
        Console.WriteLine(
            "[0] " +
            (isFirstMenu ? "Exit" : "Go back")
        );

        return choices.Length;
    }

    private int ShowEntitiesMenu()
    {
        return ReadChoice(
            ShowCustomMenu(
                "Choose service section:",
                [
                    "Manage users",
                "Manage equipment",
                "Manage rentals"
                ],
            true
            )
        );
    }

    private int ShowUserMenu()
    {
        return ReadChoice(
            ShowCustomMenu(
               "Choose users operation:",
               [
                   "Add new user",
               "List users"
               ]
            )
        );
    }

    private int ShowEquipmentMenu()
    {
        return ReadChoice(
            ShowCustomMenu(
            "Choose equipment operation:",
            [
                "Add new equipment",
            "List equipment with status",
            "List equipment available for rental",
            "Mark equipment as unavailable"
            ]
            )
        );
    }

    private int ShowRentalMenu()
    {
        return ReadChoice(
            ShowCustomMenu(
            "Choose rental operation:",
            [
                "Rent equipment to a user",
            "Return equipment",
            "Display active rentals for a selected user",
            "Display the list of overdue rentals",
            "Display service summary"
            ]
            )
        );
    }

    private User? ChooseUser()
    {
        var users = _rentalService.GetAllUsers();

        if (users.Count == 0)
        {
            Console.WriteLine("No users found.");
            return null;
        }

        string[] choices = users
            .Select(u => $"{u.FirstName} {u.LastName} ({u.GetType().Name})")
            .ToArray();

        int choice = ReadChoice(ShowCustomMenu("Select user: ", choices));
        if (choice == 0) return null;

        return users[choice - 1];
    }

    private Equipment? ChooseEquipment(bool availableOnly = false)
    {
        var equipment = availableOnly
            ? _rentalService.GetAvailableEquipment()
            : _rentalService.GetAllEquipment();

        if (equipment.Count == 0)
        {
            Console.WriteLine(availableOnly ? "No equipment available." : "No equipment found.");
            return null;
        }

        string[] choices = equipment
            .Select(e => !availableOnly
                ? $"{e.Name} ({(e.IsAvailable ? "Available" : "Unavailable")})"
                : e.Name)
            .ToArray();

        int choice = ReadChoice(ShowCustomMenu("Select equipment:", choices));
        if (choice == 0) return null;

        return equipment[choice - 1];
    }

    private Equipment? ChooseRentedEquipment()
    {
        var equipment = _rentalService.GetAllEquipment()
            .Where(e => !e.IsAvailable)
            .ToList();

        if (equipment.Count == 0)
        {
            Console.WriteLine("No rented equipment found.");
            return null;
        }

        string[] choices = equipment
            .Select(e => e.Name)
            .ToArray();

        int choice = ReadChoice(ShowCustomMenu("Select equipment:", choices));
        if (choice == 0) return null;

        return equipment[choice - 1];
    }

    private string ReadString(string prompt)
    {
        Console.Write(prompt + ": ");
        string? result;
        do
        {
            result = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(result));
        return result;
    }

    private int ReadInt(string prompt, int defaultValue)
    {
        Console.Write($"{prompt} (default: {defaultValue}): ");
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input)) return defaultValue;

        while (true)
        {
            if (int.TryParse(input, out int value))
            {
                return value;
            }
            Console.Write("Invalid number. Try again: ");
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return defaultValue;
        }
    }

    private int ChooseUserType()
    {
        return ReadChoice(
            ShowCustomMenu("Select user type:", ["Student", "Employee"])
        );
    }

    private int ChooseEquipmentType()
    {
        return ReadChoice(
            ShowCustomMenu("Select equipment type:", ["Laptop", "Projector", "Camera"])
        );
    }

    private void AddUser()
    {
        Console.WriteLine("\n=== Add New User ===");
        int userType = ChooseUserType();

        string firstName = ReadString("First name");
        string lastName = ReadString("Last name");

        User user = userType == 1
            ? new Student(firstName, lastName)
            : new Employee(firstName, lastName);

        _rentalService.AddUser(user);
        Console.WriteLine($"User added successfully! ID: {user.Id}");
    }

    private void ListUsers()
    {
        Console.WriteLine("\n=== All Users ===");
        var users = _rentalService.GetAllUsers();

        if (users.Count == 0)
        {
            Console.WriteLine("No users found.");
            return;
        }

        foreach (var u in users)
        {
            Console.WriteLine($"- {u.FirstName} {u.LastName} [{u.GetType().Name}]");
        }
        Console.WriteLine($"Total: {users.Count} user(s)");
    }

    private Laptop CreateLaptop(string name)
    {
        Console.WriteLine("Laptop specifications:");
        float storage = ReadInt("Storage (GB)", 256);
        float ram = ReadInt("RAM (GB)", 8);
        return new Laptop(name, true, storage, ram);
    }

    private Projector CreateProjector(string name)
    {
        Console.WriteLine("Projector specifications:");
        int lumens = ReadInt("Lumens", 2000);
        string resolution = ReadString("Resolution");
        return new Projector(name, true, lumens, resolution);
    }

    private Camera CreateCamera(string name)
    {
        Console.WriteLine("Camera specifications:");
        string resolution = ReadString("Resolution (e.g., 24MP)");
        Console.Write("Has flash? (yes/no, default: no): ");
        string? input = Console.ReadLine();
        bool hasFlash = input?.ToLower() == "yes";
        return new Camera(name, true, resolution, hasFlash);
    }

    private void AddEquipment()
    {
        Console.WriteLine("\n=== Add New Equipment ===");
        int type = ChooseEquipmentType();

        string name = ReadString("Name");

        Equipment eq = type switch
        {
            1 => CreateLaptop(name),
            2 => CreateProjector(name),
            3 => CreateCamera(name),
            _ => throw new InvalidOperationException("Invalid equipment type")
        };

        _rentalService.AddEquipment(eq);
        Console.WriteLine($"Equipment added successfully! ID: {eq.Id}");
    }

    private void ListAllEquipment()
    {
        Console.WriteLine("\n=== All Equipment ===");
        var equipment = _rentalService.GetAllEquipment();

        if (equipment.Count == 0)
        {
            Console.WriteLine("No equipment found.");
            return;
        }

        foreach (var e in equipment)
        {
            Console.WriteLine($"- {e.Name}: {(e.IsAvailable ? "Available" : "Unavailable")}");
        }
        Console.WriteLine($"Total: {equipment.Count} item(s)");
    }

    private void ListAvailableEquipment()
    {
        Console.WriteLine("\n=== Available Equipment ===");
        var equipment = _rentalService.GetAvailableEquipment();

        if (equipment.Count == 0)
        {
            Console.WriteLine("No equipment available.");
            return;
        }

        foreach (var e in equipment)
        {
            Console.WriteLine($"- {e.Name}");
        }
        Console.WriteLine($"Total: {equipment.Count} item(s)");
    }

    private void MarkEquipmentUnavailable()
    {
        Console.WriteLine("\n=== Mark Equipment Unavailable ===");
        var eq = ChooseEquipment();
        if (eq == null) return;

        _rentalService.MarkEquipmentUnavailable(eq);
        Console.WriteLine("Equipment marked as unavailable.");
    }

    private void RentEquipment()
    {
        Console.WriteLine("\n=== Rent Equipment ===");

        var user = ChooseUser();
        if (user == null) return;

        var eq = ChooseEquipment(availableOnly: true);
        if (eq == null) return;

        int days = ReadInt("Rental duration in days", 7);

        try
        {
            var rental = _rentalService.Rent(user, eq, DateTime.Now.AddDays(days));
            Console.WriteLine($"Success! Due date: {rental.DueDate:d}");
        }
        catch (RentalException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void ReturnEquipment()
    {
        Console.WriteLine("\n=== Return Equipment ===");

        var eq = ChooseRentedEquipment();
        if (eq == null) return;

        var rentals = _rentalService.GetAllRentals();
        var rental = rentals.FirstOrDefault(r => r.RentedEquipment.Id == eq.Id && r.ActualReturnDate == null);

        if (rental != null && rental.DueDate < DateTime.Now)
        {
            int daysOverdue = (DateTime.Now - rental.DueDate).Days;
            Console.WriteLine($"This equipment is {daysOverdue} day(s) overdue!");
            Console.WriteLine($"Penalty: {daysOverdue * RentalPolicy.OVERDUE_PENALTY_PER_DAY}");
        }

        try
        {
            _rentalService.Return(eq);
            Console.WriteLine("Equipment returned successfully!");
        }
        catch (RentalException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void ShowUserRentals()
    {
        Console.WriteLine("\n=== User's Rentals ===");

        var user = ChooseUser();
        if (user == null) return;

        var rentals = _rentalService.GetRentalsForUser(user);

        if (rentals.Count == 0)
        {
            Console.WriteLine("No rentals found for this user.");
            return;
        }

        foreach (var r in rentals)
        {
            string status = r.ActualReturnDate == null
                ? $"Active (Due: {r.DueDate:d})"
                : $"Returned on {r.ActualReturnDate:d} (Penalty: {r.Penalty})";
            Console.WriteLine($"- {r.RentedEquipment.Name}: {status}");
        }
    }

    private void ShowOverdueRentals()
    {
        Console.WriteLine("\n=== Overdue Rentals ===");

        var overdue = _rentalService.GetOverdueRentals();

        if (overdue.Count == 0)
        {
            Console.WriteLine("No overdue rentals!");
            return;
        }

        foreach (var r in overdue)
        {
            int daysLate = (DateTime.Now - r.DueDate).Days;
            Console.WriteLine($"- {r.RentedEquipment.Name} by {r.Renter.FirstName} {r.Renter.LastName}: {daysLate} day(s) overdue");
        }
    }

    private void ShowSummary()
    {
        Console.WriteLine("\n=== Service Summary ===");

        var summary = _reportService.GenerateSummary();

        Console.WriteLine($"Equipment: {summary.TotalEquipment} total, {summary.AvailableEquipment} available");
        Console.WriteLine($"Users: {summary.TotalUsers} ({summary.StudentCount} students, {summary.EmployeeCount} employees)");
        Console.WriteLine($"Rentals: {summary.TotalRentals} total, {summary.ActiveRentals} active, {summary.OverdueRentals} overdue");
    }

    private void HandleUserMenu()
    {
        int choice = ShowUserMenu();
        switch (choice)
        {
            case 1: AddUser(); break;
            case 2: ListUsers(); break;
        }
    }

    private void HandleEquipmentMenu()
    {
        int choice = ShowEquipmentMenu();
        switch (choice)
        {
            case 1: AddEquipment(); break;
            case 2: ListAllEquipment(); break;
            case 3: ListAvailableEquipment(); break;
            case 4: MarkEquipmentUnavailable(); break;
        }
    }

    private void HandleRentalMenu()
    {
        int choice = ShowRentalMenu();
        switch (choice)
        {
            case 1: RentEquipment(); break;
            case 2: ReturnEquipment(); break;
            case 3: ShowUserRentals(); break;
            case 4: ShowOverdueRentals(); break;
            case 5: ShowSummary(); break;
        }
    }

    public void Start()
    {
        Console.WriteLine("=================================");
        Console.WriteLine("   EQUIPMENT RENTAL SERVICE");
        Console.WriteLine("=================================\n");

        bool running = true;
        while (running)
        {
            int section = ShowEntitiesMenu();

            switch (section)
            {
                case 0:
                    running = false;
                    break;
                case 1:
                    HandleUserMenu();
                    break;
                case 2:
                    HandleEquipmentMenu();
                    break;
                case 3:
                    HandleRentalMenu();
                    break;
            }
        }

        Console.WriteLine("\nGoodbye!");
    }
}
