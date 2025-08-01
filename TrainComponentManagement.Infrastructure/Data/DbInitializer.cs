using TrainComponentManagement.Domain.Entities;

namespace TrainComponentManagement.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // If database already has data, exit
            if (context.Components.Any()) return;

            var components = new List<Component>
            {
                new() { Name = "Engine", UniqueNumber = "ENG123", CanAssignQuantity = false },
                new() { Name = "Passenger Car", UniqueNumber = "PAS456", CanAssignQuantity = false },
                new() { Name = "Freight Car", UniqueNumber = "FRT789", CanAssignQuantity = false },
                new() { Name = "Wheel", UniqueNumber = "WHL101", CanAssignQuantity = true, Quantity = 100 },
                new() { Name = "Seat", UniqueNumber = "STS234", CanAssignQuantity = true, Quantity = 500 },
                new() { Name = "Window", UniqueNumber = "WIN567", CanAssignQuantity = true, Quantity = 300 },
                new() { Name = "Door", UniqueNumber = "DR123", CanAssignQuantity = true, Quantity = 50 },
                new() { Name = "Control Panel", UniqueNumber = "CTL987", CanAssignQuantity = true, Quantity = 5 },
                new() { Name = "Light", UniqueNumber = "LGT456", CanAssignQuantity = true, Quantity = 100 },
                new() { Name = "Brake", UniqueNumber = "BRK789", CanAssignQuantity = true, Quantity = 20 },
                new() { Name = "Bolt", UniqueNumber = "BLT321", CanAssignQuantity = true, Quantity = 2000 },
                new() { Name = "Nut", UniqueNumber = "NUT654", CanAssignQuantity = true, Quantity = 2000 },
                new() { Name = "Engine Hood", UniqueNumber = "EH789", CanAssignQuantity = false },
                new() { Name = "Axle", UniqueNumber = "AX456", CanAssignQuantity = false },
                new() { Name = "Piston", UniqueNumber = "PST789", CanAssignQuantity = false },
                new() { Name = "Handrail", UniqueNumber = "HND234", CanAssignQuantity = true, Quantity = 30 },
                new() { Name = "Step", UniqueNumber = "STP567", CanAssignQuantity = true, Quantity = 30 },
                new() { Name = "Roof", UniqueNumber = "RF123", CanAssignQuantity = false },
                new() { Name = "Air Conditioner", UniqueNumber = "AC789", CanAssignQuantity = false },
                new() { Name = "Flooring", UniqueNumber = "FLR456", CanAssignQuantity = false },
                new() { Name = "Mirror", UniqueNumber = "MRR789", CanAssignQuantity = true, Quantity = 10 },
                new() { Name = "Horn", UniqueNumber = "HRN321", CanAssignQuantity = false },
                new() { Name = "Coupler", UniqueNumber = "CPL654", CanAssignQuantity = false },
                new() { Name = "Hinge", UniqueNumber = "HNG987", CanAssignQuantity = true, Quantity = 50 },
                new() { Name = "Ladder", UniqueNumber = "LDR456", CanAssignQuantity = true, Quantity = 5 },
                new() { Name = "Paint", UniqueNumber = "PNT789", CanAssignQuantity = false },
                new() { Name = "Decal", UniqueNumber = "DCL321", CanAssignQuantity = true, Quantity = 100 },
                new() { Name = "Gauge", UniqueNumber = "GGS654", CanAssignQuantity = true, Quantity = 10 },
                new() { Name = "Battery", UniqueNumber = "BTR987", CanAssignQuantity = false },
                new() { Name = "Radiator", UniqueNumber = "RDR456", CanAssignQuantity = false },
            };

            context.Components.AddRange(components);
            context.SaveChanges();
        }
    }
}
