using Tutorial3.Services;
using Tutorial3.Entities;
using Tutorial3.ui;

RentalService rentalService = new RentalService();
ReportService reportService = new ReportService(rentalService);
Menu menu = new Menu(rentalService, reportService);
menu.Start();
