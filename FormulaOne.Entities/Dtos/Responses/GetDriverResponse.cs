namespace FormulaOne.Entities.Dtos.Responses;

public class GetDriverResponse
{
    public Guid DriverId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }    
}