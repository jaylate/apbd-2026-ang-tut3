# Equipment Rental Service

A console application for university equipment rental management.

## How to run
- `dotnet run --project Tutorial3`
- Interactive menu - select options with numbers

## Design
### File Structure
- Classes are separated into folders each having meaning:
  - `Entities/`: Data models - `Equipment` types (Laptop, Camera, Projector), `User` types (Student, Employee), `Rental`, `RentalPolicy`
  - `Services/`: Business logic - `RentalService` handles all rental operations, `ReportService` generates structured reports
  - `Exceptions/`: Custom exceptions extending `RentalException` class
  - `ui/`: Console menu handling
### Cohesion
Each class has one clear responsibility:
- RentalService handles all rental operations
- ReportService generates all reports
- Menu handles all user interaction
- RentalPolicy stores configuration values
### Coupling
Loose coupling achieved via interfaces:
- IRentalService allows swapping rental implementations
- IReportService allows different report formats
- Menu depends on interfaces, enabling dependency injection
### SOLID Principles implemented
- **S** (SRP): Each class has one job
- **O** (OCP): Custom exceptions allow adding new error types without modifying existing code
- **I** (ISP): Interfaces are focused on the required functionality - `IRentalService` groups rental operations, `IReportService` handles summary generation
- **L** (LSP): Student and Employee inherit `MaxRentals` from User, enabling polymorphic rental limit checking
- **D** (DIP): Menu depends on abstractions (interfaces), not concrete classes
### Business Rules
All rental limits and penalties are defined in `RentalPolicy`:
- Student max rentals: 2
- Employee max rentals: 5
- Penalty per overdue day: 10
