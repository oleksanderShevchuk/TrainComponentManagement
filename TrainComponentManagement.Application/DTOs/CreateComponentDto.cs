namespace TrainComponentManagement.Application.DTOs
{
    public class CreateComponentDto
    {
        public string Name { get; set; } = string.Empty;
        public string UniqueNumber { get; set; } = string.Empty;
        public bool CanAssignQuantity { get; set; }
    }
}
