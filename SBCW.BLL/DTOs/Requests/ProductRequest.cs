namespace SBCW.BLL.DTOs.Requests;

public class ProductRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Link { get; set; }
    public string Type { get; set; }
}